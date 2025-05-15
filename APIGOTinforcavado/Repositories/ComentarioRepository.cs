using APIGOTinforcavado.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Shared.models;

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
            var sql = @"
                INSERT INTO Comentario (conteudo, data, UtilizadorId, TicketId)
                VALUES (@conteudo, @data, @UtilizadorId, @TicketId);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            comentario.id = await connection.ExecuteScalarAsync<int>(sql, comentario);
            return comentario;
        }

        public async Task<List<Comentario>> GetAllAsync()
        {
            var sql = "SELECT * FROM Comentario";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var comentarios = await connection.QueryAsync<Comentario>(sql);
            return comentarios.ToList();
        }

        public async Task<Comentario?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Comentario WHERE id = @id";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<Comentario>(sql, new { id });
        }

        public async Task<List<Comentario>> GetByTicketIdAsync(int ticketId)
        {
            var sql = "SELECT * FROM Comentario WHERE TicketId = @ticketId";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var comentarios = await connection.QueryAsync<Comentario>(sql, new { ticketId });
            return comentarios.ToList();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Comentario WHERE id = @id";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var affected = await connection.ExecuteAsync(sql, new { id });
            return affected > 0;
        }
    }
}
