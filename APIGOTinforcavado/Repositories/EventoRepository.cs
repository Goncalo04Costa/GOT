using APIGOTinforcavado.Data;
using Microsoft.EntityFrameworkCore;
using Shared.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIGOTinforcavado.Repositories
{
    public class EventoRepository
    {
        private readonly AppDbContext _context;

        public EventoRepository(AppDbContext context)
        {
            _context = context;
        }

        // Criar um novo evento
        public async Task<Evento> CreateAsync(Evento evento)
        {
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
            return evento;
        }
        public async Task<List<Evento>> GetEventosByTicketIdAsync(string ticketId)
        {
            return await _context.Eventos
                .Where(c => c.TicketId == ticketId)
                .ToListAsync();
        }

        public async Task<Evento?> GetByIdAsync(int id)
        {
            return await _context.Eventos.FirstOrDefaultAsync(e => e.Id == id);
        }

        // Listar todos os eventos
        public async Task<List<Evento>> GetAllAsync()
        {
            return await _context.Eventos.ToListAsync();
        }

        // Atualizar evento
        public async Task<bool> UpdateAsync(Evento evento)
        {
            _context.Eventos.Update(evento);
            return await _context.SaveChangesAsync() > 0;
        }

        // Excluir evento
        public async Task<bool> DeleteAsync(Evento evento)
        {
            _context.Eventos.Remove(evento);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}