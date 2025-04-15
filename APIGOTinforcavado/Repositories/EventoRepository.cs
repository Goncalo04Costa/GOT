using APIGOTinforcavado.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;
using Shared.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGOTinforcavado.Repositories
{
    public class EventoRepository
    {
        private readonly string _connectionString;

        public EventoRepository(IOptions<ConnectionStrings> options)
        {
            _connectionString = options.Value.DefaultConnection;
        }

        // Criar novo evento
        public async Task<Evento> CreateAsync(Evento evento)
        {
            try
            {
                var sql = @"
                    INSERT INTO Eventos (Data, evento, TicketId, UtilizadorId)
                    OUTPUT INSERTED.Id
                    VALUES (@Data, @DescricaoEvento, @TicketId, @UtilizadorId)";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var id = await connection.ExecuteScalarAsync<int>(sql, new
                    {
                        evento.Data,
                        DescricaoEvento = evento.evento,
                        evento.TicketId,
                        evento.UtilizadorId
                    });

                    evento.Id = id;
                    return evento;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar o evento.", ex);
            }
        }

        // Obter evento por ID
        public async Task<Evento?> GetByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT * FROM Eventos WHERE Id = @Id";
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return await connection.QueryFirstOrDefaultAsync<Evento>(sql, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar o evento pelo ID.", ex);
            }
        }

        // Obter todos os eventos
        public async Task<List<Evento>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM Eventos";
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return (await connection.QueryAsync<Evento>(sql)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar todos os eventos.", ex);
            }
        }

        // Obter eventos por ID do ticket
        public async Task<List<Evento>> GetEventosByTicketIdAsync(string ticketId)
        {
            try
            {
                if (!int.TryParse(ticketId, out int ticketIdConvertido))
                    throw new ArgumentException("O ID do ticket fornecido não é válido.");

                var sql = "SELECT * FROM Eventos WHERE TicketId = @TicketId";
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return (await connection.QueryAsync<Evento>(sql, new { TicketId = ticketIdConvertido })).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar eventos por ID do ticket.", ex);
            }
        }

        // Atualizar evento
        public async Task<bool> UpdateAsync(Evento evento)
        {
            try
            {
                var sql = @"
                    UPDATE Eventos 
                    SET Data = @Data, 
                        evento = @DescricaoEvento, 
                        TicketId = @TicketId, 
                        UtilizadorId = @UtilizadorId
                    WHERE Id = @Id";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var linhasAfetadas = await connection.ExecuteAsync(sql, new
                    {
                        evento.Data,
                        DescricaoEvento = evento.evento,
                        evento.TicketId,
                        evento.UtilizadorId,
                        evento.Id
                    });

                    return linhasAfetadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o evento.", ex);
            }
        }

        // Eliminar evento
        public async Task<bool> DeleteAsync(Evento evento)
        {
            try
            {
                var sql = "DELETE FROM Eventos WHERE Id = @Id";
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var linhasAfetadas = await connection.ExecuteAsync(sql, new { evento.Id });
                    return linhasAfetadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao eliminar o evento.", ex);
            }
        }
    }
}
