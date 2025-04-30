using APIGOTinforcavado.Repositories;
using Shared.models;
using System.Threading.Tasks;

namespace APIGOTinforcavado.Services
{
    public class UtilizadorService
    {
        private readonly UtilizadorRepository _utilizadorRepository;
    

     

     

        public async Task<Utilizador> GetUtilizadorByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.", nameof(id));

            try
            {
                return await _utilizadorRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao procurar o utilizador com ID {id}.", ex);
            }
        }


        public async Task<List<Utilizador>> GetUtilizadoresAsync()
        {
            try
            {
                return await _utilizadorRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao procurar os utilizadores.", ex);
            }
        }
    }
}
