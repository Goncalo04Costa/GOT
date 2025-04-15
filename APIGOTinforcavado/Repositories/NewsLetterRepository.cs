using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;
using Shared.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using APIGOTinforcavado.Models;

namespace APIGOTinforcavado.Repositories
{
    public class NewsLetterRepository
    {
        private readonly string _connectionString;

        public NewsLetterRepository(IOptions<ConnectionStrings> options)
        {
            _connectionString = options.Value.DefaultConnection;
        }


        public async Task<NewsLetter> CreateAsync(NewsLetter newsLetter)
        {
            try
            {
                    var sql = @"INSERT INTO NewsLetter (Email)
                                VALUES (@Email);
                                SELECT SCOPE_IDENTITY();";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var id = await connection.ExecuteScalarAsync<int>(sql, new { newsLetter.Email });
                    newsLetter.Id = id;
                    return newsLetter;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar email à newsletter.", ex);
            }
        }


        public async Task<List<NewsLetter>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM NewsLetter";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return (await connection.QueryAsync<NewsLetter>(sql)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar emails da newsletter.", ex);
            }
        }

    
        public async Task<bool> DeleteAsync(string email)
        {
            try
            {
                var sql = "DELETE FROM NewsLetter WHERE Email = @Email";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, new { Email = email });
                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover email da newsletter.", ex);
            }
        }
    }
}
