using Shared.models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGOTinforcavado.Models;

namespace APIGOTinforcavado.Repositories
{
    public class ChatBotMessagesRepository
    {
        private readonly string _connectionString;

        public ChatBotMessagesRepository(IOptions<ConnectionStrings> options)
        {
            _connectionString = options.Value.DefaultConnection;
        }

        public async Task<ChatBotMessages?> ObterMensagemPorPalavraAsync(string mensagem)
        {
            try
            {
                var palavras = mensagem.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var sql = "SELECT TOP 1 * FROM ChatBotMessages WHERE ";
                sql += string.Join(" OR ", palavras.Select((p, i) => $"PalavrasChave LIKE @p{i}"));

                var parametros = new DynamicParameters();
                for (int i = 0; i < palavras.Length; i++)
                {
                    parametros.Add($"@p{i}", $"%{palavras[i]}%");
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return await connection.QueryFirstOrDefaultAsync<ChatBotMessages>(sql, parametros);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar mensagem do chatbot.", ex);
            }
        }

        public async Task<List<ChatBotMessages>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM ChatBotMessages";
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return (await connection.QueryAsync<ChatBotMessages>(sql)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter todas as mensagens do chatbot.", ex);
            }
        }

        public async Task<ChatBotMessages> CreateAsync(ChatBotMessages message)
        {
            try
            {
                var sql = @"INSERT INTO ChatBotMessages (PalavrasChave, Message) 
                            OUTPUT INSERTED.Id 
                            VALUES (@PalavrasChave, @Message)";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var id = await connection.ExecuteScalarAsync<int>(sql, message);
                    message.Id = id;
                    return message;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir mensagem no chatbot.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var sql = "DELETE FROM ChatBotMessages WHERE Id = @Id";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var rows = await connection.ExecuteAsync(sql, new { Id = id });
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao eliminar mensagem do chatbot.", ex);
            }
        }
    }
}
