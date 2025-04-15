using Microsoft.AspNetCore.Mvc;
using APIGOTinforcavado.Services;
using Shared.models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace APIGOTinforcavado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly EventoService _eventoService;

        public EventoController(EventoService eventoService)
        {
            _eventoService = eventoService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateEvento([FromBody] Evento evento)
        {
            if (evento == null)
                return BadRequest("Os dados do comentário são inválidos.");

            var createdEvento = await _eventoService.CreateEventoAsync(evento);
            return CreatedAtAction(nameof(GetEventoById), new { id = createdEvento.Id }, createdEvento);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoById(int id)
        {
            var evento = await _eventoService.GetEventoByIdAsync(id);
            if (evento == null)
                return NotFound();

            return Ok(evento);
        }

        [HttpGet("ticket/{ticketId}")]
        public async Task<IActionResult> GetComentariosByTicketId(string ticketId)
        {
            var eventos = await _eventoService.GetEventosByTicketIdAsync(ticketId);
            if (eventos == null || !eventos.Any())
                return NotFound(new { Message = $"Nenhum evento encontrado para o TicketId {ticketId}." });

            return Ok(eventos);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventos()
        {
            var eventos = await _eventoService.GetEventosAsync();
            return Ok(eventos);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvento(int id, [FromBody] Evento evento)
        {
            if (id != evento.Id)
                return BadRequest("O ID do evento não confere.");

            var updatedEvento = await _eventoService.UpdateEventoAsync(id, evento);
            if (updatedEvento == null)
                return NotFound();

            return Ok(updatedEvento);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var result = await _eventoService.DeleteEventoAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
