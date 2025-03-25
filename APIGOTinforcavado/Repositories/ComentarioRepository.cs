using APIGOTinforcavado.Data;
using Microsoft.EntityFrameworkCore;
using Shared.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIGOTinforcavado.Repositories
{
    public class ComentarioRepository
    {
        private readonly AppDbContext _context;

        public ComentarioRepository(AppDbContext context)
        {
            _context = context;
        }

        // Criar um novo comentário
        public async Task<Comentario> CreateAsync(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();
            return comentario;
        }

        
        public async Task<Comentario?> GetByIdAsync(int id)
        {
            return await _context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);
        }

        // Listar todos os comentários
        public async Task<List<Comentario>> GetAllAsync()
        {
            return await _context.Comentarios.ToListAsync();
        }

        public async Task<List<Comentario>> GetComentariosByTicketIdAsync(string ticketId)
        {
            return await _context.Comentarios
                .Where(c => c.TicketId == ticketId)
                .ToListAsync();
        }

        // Atualizar comentário
        public async Task<bool> UpdateAsync(Comentario comentario)
        {
            _context.Comentarios.Update(comentario);
            return await _context.SaveChangesAsync() > 0;
        }

        // Excluir comentário
        public async Task<bool> DeleteAsync(Comentario comentario)
        {
            _context.Comentarios.Remove(comentario);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
