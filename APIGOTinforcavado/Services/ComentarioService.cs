using APIGOTinforcavado.Repositories;
using Shared.models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MudBlazor.Colors;

namespace APIGOTinforcavado.Services
{
    public class ComentarioService
    {
        private readonly ComentarioRepository _comentarioRepository;

        public ComentarioService(ComentarioRepository comentarioRepository)
        {
            _comentarioRepository = comentarioRepository;
        }

        
        public async Task<ComentarioTicket> CreateComentarioAsync(ComentarioTicket comentario)
        {
            if (comentario == null)
                throw new ArgumentNullException(nameof(comentario));

            try
            {
                comentario.Data = DateTime.UtcNow;

               
                if (comentario.Ticket != null)
                {
                    comentario.Ticket = null;
                }

                var createdComentario = await _comentarioRepository.CreateAsync(comentario);
                return createdComentario;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao criar o comentário.", ex);
            }
        }



        public async Task<ComentarioTicket> GetComentarioByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.", nameof(id));

            try
            {
                return await _comentarioRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao procurar o comentário com ID {id}.", ex);
            }
        }

      
        public async Task<List<ComentarioTicket>> GetComentariosAsync()
        {
            try
            {
                return await _comentarioRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao procurar os comentários.", ex);
            }
        }



        public async Task<List<ComentarioTicket>> GetComentariosByTicketIdAsync(string ticketId)
        {
            try
            {
                return await _comentarioRepository.GetComentariosByTicketIdAsync(ticketId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao procurar os comentários para o ticket com ID {ticketId}.", ex);
            }
        }


        public async Task<List<ComentarioTicket>> GetComentariosPorCodigoTicketAsync(string codigoTicket)
        {
            try
            {
                return await _comentarioRepository.GetComentariosByCodigoTicketAsync(codigoTicket);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao procurar comentários para o ticket com código {codigoTicket}.", ex);
            }
        }
        


        public async Task<bool> DeleteComentarioAsync(int id)
        {
            try
            {
                var comentario = await _comentarioRepository.GetByIdAsync(id);
                if (comentario == null)
                    return false;

                return await _comentarioRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao eliminar o comentário com ID {id}.", ex);
            }
        }
    }
}
