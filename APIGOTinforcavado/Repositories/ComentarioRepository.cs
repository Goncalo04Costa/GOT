using APIGOTinforcavado.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;
using Shared.models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace APIGOTinforcavado.Repositories
{
    public class ComentarioRepository
    {
        private readonly string _connectionString;

        public ComentarioRepository(IOptions<ConnectionStrings> options)
        {
            _connectionString = options.Value.DefaultConnection;
        }


        public async Task<Comentario> CreateAsync(Comentario comentario)
        {
            try
            {
                var sql = @"INSERT INTO Comentarios (TicketId, Texto, Data) 
                            OUTPUT INSERTED.Id 
                            VALUES (@TicketId, @Texto, @Data)";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var result = await connection.ExecuteScalarAsync<int>(sql, new
                    {
                        comentario.TicketId,
                        comentario.Conteudo,
                        comentario.Data
                    });

                    comentario.Id = result;
                    return comentario;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar comentário.", ex);
            }
        }


        public async Task<Comentario?> GetByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT * FROM Comentarios WHERE Id = @Id";
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return await connection.QueryFirstOrDefaultAsync<Comentario>(sql, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar comentário por ID.", ex);
            }
        }

        // Obter todos os comentários
        public async Task<List<Comentario>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM Comentarios";
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return (await connection.QueryAsync<Comentario>(sql)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar todos os comentários.", ex);
            }
        }

        // Obter comentários por TicketId
        public async Task<List<Comentario>> GetComentariosByTicketIdAsync(string ticketId)
        {
            try
            {
                if (!int.TryParse(ticketId, out int parsedTicketId))
                {
                    throw new ArgumentException("O ID do ticket fornecido não é válido.");
                }

                var sql = "SELECT * FROM Comentarios WHERE TicketId = @TicketId";
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return (await connection.QueryAsync<Comentario>(sql, new { TicketId = parsedTicketId })).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar comentários pelo TicketId.", ex);
            }
        }

        // Obter comentários por Código do Ticket
        public async Task<List<Comentario>> GetComentariosByCodigoTicketAsync(string codigoTicket)
        {
            try
            {
                var sql = @"
                    SELECT c.* 
                    FROM Comentarios c
                    JOIN Tickets t ON c.TicketId = t.Id
                    WHERE t.Codigo = @CodigoTicket";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return (await connection.QueryAsync<Comentario>(sql, new { CodigoTicket = codigoTicket })).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar comentários pelo código do ticket.", ex);
            }
        }

   
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var sql = "DELETE FROM Comentarios WHERE Id = @Id";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao eliminar comentário.", ex);
            }
        }
    }
}
