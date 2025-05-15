using APIGOTinforcavado.Repositories;
using Shared.models;

namespace APIGOTinforcavado.Services
{
    public class ComentarioService
    {
        private readonly ComentarioRepository _comentarioRepository;

        public ComentarioService(ComentarioRepository comentarioRepository)
        {
            _comentarioRepository = comentarioRepository;
        }

        public async Task<Comentario> CreateComentarioAsync(Comentario comentario)
        {
            if (comentario == null)
                throw new ArgumentNullException(nameof(comentario));

            comentario.data = DateTime.UtcNow;

            try
            {
                return await _comentarioRepository.CreateAsync(comentario);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao criar comentário.", ex);
            }
        }

        public async Task<List<Comentario>> GetAllComentariosAsync()
        {
            try
            {
                return await _comentarioRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao buscar todos os comentários.", ex);
            }
        }

        public async Task<Comentario?> GetComentarioByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido para comentário.", nameof(id));

            try
            {
                return await _comentarioRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao buscar comentário com ID {id}.", ex);
            }
        }

        public async Task<List<Comentario>> GetComentariosByTicketIdAsync(int ticketId)
        {
            try
            {
                return await _comentarioRepository.GetByTicketIdAsync(ticketId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao buscar comentários do ticket {ticketId}.", ex);
            }
        }

        public async Task<bool> DeleteComentarioAsync(int id)
        {
            try
            {
                return await _comentarioRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao apagar comentário com ID {id}.", ex);
            }
        }
    }
}
