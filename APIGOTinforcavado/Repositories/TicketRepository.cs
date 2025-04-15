using APIGOTinforcavado.Data;
using APIGOTinforcavado.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.models;
using System;
using static MudBlazor.Colors;

namespace APIGOTinforcavado.Repositories
{
    public class TicketRepository
    {
        //private readonly AppDbContext _context;

        //public TicketRepository(AppDbContext context)
        //{
        //    _context = context;
        //}

        private readonly string _connectionString;

        public TicketRepository(IOptions<ConnectionStrings> options)
        {
            _connectionString = options.Value.DefaultConnection;
        }


        // Criar um novo Ticket
        public async Task<Ticket> CreateAsync(Ticket ticket)
        {
            try
            {
                var sql = @"
            INSERT INTO Tickets (Codigo, Data, Nome, Empresa, Email, Assunto, Mensagem, Departamento, TipoTicket, EstadoTarefa, UtilizadorId, Telefone)
            VALUES (@Codigo, @Data, @Nome, @Empresa, @Email, @Assunto, @Mensagem, @Departamento, @TipoTicket, @EstadoTarefa, @UtilizadorId, @Telefone);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var id = await connection.ExecuteScalarAsync<int>(sql, ticket);
                    ticket.Id = id;
                    return ticket;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar ticket.", ex);
            }
        }


        public async Task<Ticket?> GetByCodigoAsync(string codigo)
        {
            try
            {
                var sql = "SELECT * FROM Tickets WHERE Codigo = @Codigo"; // Tabela corrigida

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return await connection.QueryFirstOrDefaultAsync<Ticket>(sql, new { Codigo = codigo });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao procurar ticket pelo código '{codigo}'.", ex);
            }

            //return await _context.Tickets.FirstOrDefaultAsync(t => t.codigo == codigo);
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT * FROM Tickets WHERE id = @id"; 

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return await connection.QueryFirstOrDefaultAsync<Ticket>(sql, new { id = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao procurar ticket pelo id '{id}'.", ex);
            }
            //return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
        }



   

        // procurar todos os Tickets
        public async Task<List<Ticket>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM Tickets";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return (await connection.QueryAsync<Ticket>(sql)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar todos os  emails da newsletter.", ex);
            }
        }

        // procurar Tickets pelo telefone
        public async Task<List<Ticket>> SearchByPhoneAsync(string telefone)
        {

            try
            {
                var sql = "SELECT * FROM Ticket WHERE Telefone LIKE @Telefone";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return (await connection.QueryAsync<Ticket>(
                        sql,
                        new { Telefone = $"%{telefone}%" }
                    )).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar tickets pelo numero de telefone.", ex);
            }


            //return await _context.Tickets
            //    .Where(t =>t.Telefone.ToString().Contains(telefone))
            //    .ToListAsync();

        }



        public async Task AddFileToTicketAsync(UploadedFiles uploadedFile)
        {
            try
            {
                var sql = @"
            INSERT INTO UploadedFiles (FileType, FileData, TicketId)
            VALUES (@FileType, @FileData, @TicketId)";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    await connection.ExecuteAsync(sql, new
                    {
                        uploadedFile.FileType,
                        uploadedFile.FileData,
                        uploadedFile.TicketId
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar ficheiro ao ticket.", ex);
            }
        }

        // Atualizar um Ticket existente
        public async Task<Ticket> UpdateAsync(Ticket ticket)
        {
            try
            {
                var sql = @"
                UPDATE Tickets
                SET 
                    Codigo = @Codigo,
                    Data = @Data,
                    Nome = @Nome,
                    Empresa = @Empresa,
                    Email = @Email,
                    Assunto = @Assunto,
                    Mensagem = @Mensagem,
                    Departamento = @Departamento,
                    TipoTicket = @TipoTicket,
                    EstadoTarefa = @EstadoTarefa,
                    UtilizadorId = @UtilizadorId,
                    Telefone = @Telefone
                WHERE Id = @Id";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, ticket);

                    if (affectedRows == 0)
                    {
                        throw new Exception($"Nenhum ticket encontrado com o ID {ticket.Id} para atualizar.");
                    }

                    return ticket;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar ticket com ID {ticket.Id}.", ex);
            }
        }


        // procurar ficheiro pelo ID
        public async Task<UploadedFiles?> GetFileByIdAsync(int fileId)
        {
            try
            {
                var sql = "SELECT * FROM UploadedFiles WHERE Id = @Id";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return await connection.QueryFirstOrDefaultAsync<UploadedFiles>(sql, new { Id = fileId });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter ficheiro com ID {fileId}.", ex);
            }
            //return await _context.UploadedFiles
            //    .FirstOrDefaultAsync(f => f.Id == fileId);
        }

        

 

    }
}
