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
    public class UtilizadorRepository
    {
        private readonly string _connectionString;

        public UtilizadorRepository(IOptions<ConnectionStrings> options)
        {
            _connectionString = options.Value.DefaultConnection;
        }

        // Criar novo utilizador
        public async Task<Utilizador> CreateAsync(Utilizador utilizador)
        {
            try
            {
                var sql = @"
                    INSERT INTO Utilizadores (Email, Password, Role, Nome)
                    VALUES (@Email, @Password, @Role, @Nome);
                    SELECT CAST(SCOPE_IDENTITY() as int);
                ";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var id = await connection.ExecuteScalarAsync<int>(sql, utilizador);
                    utilizador.Id = id;
                    return utilizador;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar o utilizador.", ex);
            }
        }


        // Obter utilizador por email
        public async Task<Utilizador?> GetByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return null;

                var sql = @"
            SELECT u.*, e.Id, e.Nome
            FROM Utilizadores u
            INNER JOIN Empresa e ON u.EmpresaId = e.Id
            WHERE u.Email = @Email";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var utilizador = await connection.QueryAsync<Utilizador, Empresa, Utilizador>(
                        sql,
                        (u, e) =>
                        {
                            u.Empresa = e;
                            return u;
                        },
                        new { Email = email },
                        splitOn: "Id"
                    );

                    return utilizador.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar o utilizador pelo email.", ex);
            }
        }


        // Obter utilizador por ID
        public async Task<Utilizador?> GetByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT * FROM Utilizadores WHERE Id = @Id";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return await connection.QueryFirstOrDefaultAsync<Utilizador>(sql, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar o utilizador pelo ID.", ex);
            }
        }

        // Obter utilizador por ID
        public async Task<Utilizador?> GetEmpresaByIdAsync(int id)
        {
            try
            {
                var sql = "SELECT * FROM Empresa WHERE Id = @Id";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return await connection.QueryFirstOrDefaultAsync<Utilizador>(sql, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar a empresa pelo ID.", ex);
            }
        }

        // Obter todos os utilizadores
        public async Task<List<Utilizador>> GetAllAsync()
        {
            try
            {
                var sql = "SELECT * FROM Utilizadores";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return (await connection.QueryAsync<Utilizador>(sql)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar todos os utilizadores.", ex);
            }
        }


        public async Task UpdateByEmailAsync(Utilizador utilizador)
        {
            try
            {
                var sql = @"
            UPDATE Utilizadores
            SET Nome = @Nome,
                Password = @Password
            WHERE Email = @Email;
        ";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    await connection.ExecuteAsync(sql, new
                    {
                        Nome = utilizador.Nome,
                        Password = utilizador.Password,
                        Email = utilizador.Email
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o utilizador pelo email.", ex);
            }
        }


        public async Task<List<Utilizador>> GetByEmpresaIdAsync(int empresaId)
        {
            try
            {
                var sql = "SELECT * FROM Utilizadores WHERE EmpresaId = @EmpresaId";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var utilizadores = await connection.QueryAsync<Utilizador>(sql, new { EmpresaId = empresaId });
                    return utilizadores.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao procurar utilizadores pela EmpresaId.", ex);
            }
        }

    }
}
