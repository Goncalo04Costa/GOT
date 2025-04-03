using APIGOTinforcavado.Repositories;
using Shared.models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIGOTinforcavado.Services
{
    public class EventoService
    {
        private readonly EventoRepository _eventoRepository;

        public EventoService(EventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        // Criar novo evento e definir data de criação
        public async Task<Evento> CreateEventoAsync(Evento evento)
        {
            if (evento == null)
                throw new ArgumentNullException(nameof(evento));

            try
            {
                evento.Data = DateTime.UtcNow;

               
                if (evento.Ticket != null)
                {
                    evento.Ticket = null;
                }

                var createdEvento = await _eventoRepository.CreateAsync(evento);
                return createdEvento;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao criar o evento.", ex);
            }
        }

        // Obter evento por ID
        public async Task<Evento> GetEventoByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.", nameof(id));

            try
            {
                return await _eventoRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao procurar o evento com ID {id}.", ex);
            }
        }

        // Listar todos os eventos
        public async Task<List<Evento>> GetEventosAsync()
        {
            try
            {
                return await _eventoRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao procurar os eventos.", ex);
            }
        }


        public async Task<Evento> UpdateEventoAsync(int id, Evento updatedEvento)
        {
            if (id != updatedEvento.Id)
                throw new ArgumentException("IDs não coincidem.", nameof(id));

            try
            {
                var existingComentario = await _eventoRepository.GetByIdAsync(id);
                if (existingComentario == null)
                    return null;


                existingComentario.evento = updatedEvento.evento;
                existingComentario.TicketId = updatedEvento.TicketId;
                existingComentario.UtilizadorId = updatedEvento.UtilizadorId;


                var success = await _eventoRepository.UpdateAsync(existingComentario);
                return success ? existingComentario : null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao atualizar o evento com ID {id}.", ex);
            }
        }


        public async Task<bool> DeleteEventoAsync(int id)
        {
            try
            {
                var evento = await _eventoRepository.GetByIdAsync(id);
                if (evento == null)
                    return false;

                return await _eventoRepository.DeleteAsync(evento);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao eliminar o evento com ID {id}.", ex);
            }
        }

        public async Task<List<Evento>> GetEventosByTicketIdAsync(string ticketId)
        {
            try
            {
                return await _eventoRepository.GetEventosByTicketIdAsync(ticketId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao procurar os eventos para o ticket com ID {ticketId}.", ex);
            }
        }



    }
}
