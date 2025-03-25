using APIGOTinforcavado.Data;
using Microsoft.EntityFrameworkCore;
using Shared.models;
using System.Linq;
using System.Threading.Tasks;

namespace APIGOTinforcavado.Repositories
{
    public class UtilizadorRepository
    {
        private readonly AppDbContext _context;

        public UtilizadorRepository(AppDbContext context)
        {
            _context = context;
        }

   
        public async Task<Utilizador> GetByEmailAsync(string email)
        {
           
            if (string.IsNullOrEmpty(email))
                return null;  
 
            return await _context.utilizadores.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Utilizador?> GetByIdAsync(int id)
        {
            return await _context.utilizadores.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
