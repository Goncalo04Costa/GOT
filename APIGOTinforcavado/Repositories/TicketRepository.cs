using APIGOTinforcavado.Data;
using Microsoft.EntityFrameworkCore;
using Shared.models;
using System;

namespace APIGOTinforcavado.Repositories
{
    public class TicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        // Criar um novo Ticket
        public async Task<Ticket> CreateAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        // procurar um Ticket pelo ID
        public async Task<Ticket?> GetByIdAsync(string id)
        {
            return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
        }

        // Atualizar um Ticket
        public async Task<bool> UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            return await _context.SaveChangesAsync() > 0;
        }

        // procurar todos os Tickets
        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        // procurar Tickets pelo telefone
        public async Task<List<Ticket>> SearchByPhoneAsync(string telefone)
        {
            return await _context.Tickets
                .Where(t =>t.Telefone.ToString().Contains(telefone))
                .ToListAsync();
        }

        // procurar Tickets pelo código
        public async Task<List<Ticket>> SearchByCodeAsync(string codigo)
        {
            return await _context.Tickets
                .Where(t => t.Id == codigo)
                .ToListAsync();
        }


        public async Task AddFileToTicketAsync(UploadedFiles uploadedFile)
        {
            _context.UploadedFiles.Add(uploadedFile);
            await _context.SaveChangesAsync();
        }

        // procurar ficheiro pelo ID
        public async Task<UploadedFiles?> GetFileByIdAsync(int fileId)
        {
            return await _context.UploadedFiles
                .FirstOrDefaultAsync(f => f.Id == fileId);
        }
    }
}
