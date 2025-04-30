using Microsoft.AspNetCore.Mvc;
using APIGOTinforcavado.Services;
using Shared.models;
using Microsoft.EntityFrameworkCore;
using static Shared.models.Ticket;

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
                await _ticketService.UploadFilesAsync(createdTicket.Id, ficheiros);  // Save files
            }

            return CreatedAtAction(nameof(GetTicketById), new { id = createdTicket.Id }, createdTicket);
        }

        // Procurar ticket por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateTicketStatus(string id, EstadoTicket status)
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

        [HttpGet("search/{codigo}")]
        public async Task<ActionResult<Ticket>> GetTicketByCodigo(string codigo)
        {
            var ticket = await _ticketService.GetTicketByCodigoAsync(codigo);

            if (ticket == null)
                return NotFound();

            return ticket;
        }


        // Download de ficheiro
        [HttpGet("ficheiro/{fileId}")]
        public async Task<IActionResult> DownloadFile(int fileId)
        {
            try
            {
                var file = await _ticketService.GetFileByIdAsync(fileId);
                if (file == null)
                    return NotFound();

                return File(file.FileData, file.FileType, file.NomeFicheiro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao fazer download do ficheiro: {ex.Message}");
            }
        }
    }
}