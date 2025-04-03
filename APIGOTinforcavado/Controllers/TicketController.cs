using Microsoft.AspNetCore.Mvc;
using APIGOTinforcavado.Services;
using Shared.models;

namespace APIGOTinforcavado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromForm] Ticket ticket, [FromForm] List<IFormFile> ficheiros)
        {
            if (ticket == null)
                return BadRequest("Os dados do ticket são inválidos.");

            var createdTicket = await _ticketService.CreateTicketAsync(ticket);

            if (ficheiros != null && ficheiros.Count > 0)
            {
                await _ticketService.UploadFilesAsync(createdTicket.Id, ficheiros);
            }

            return CreatedAtAction(nameof(GetTicketById), new { id = createdTicket.Id }, createdTicket);
        }


        // Procurar ticket por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(string id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        // Atualizar os estados dos tickets
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateTicketStatus(string id, [FromBody] EstadoTarefa status)
        {
            var updatedTicket = await _ticketService.UpdateTicketStatusAsync(id, status);
            if (updatedTicket == null)
                return NotFound();

            return Ok(updatedTicket);
        }

        // Obter todos os tickets
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _ticketService.GetTicketsAsync();
            return Ok(tickets);
        }

        // Procurar tickets pelo código
        [HttpGet("search")]
        public async Task<IActionResult> SearchTicketsByCode([FromQuery] string codigo)
        {
            var tickets = await _ticketService.SearchTicketsByCodeAsync(codigo);
            return Ok(tickets);
        }
    }
}
