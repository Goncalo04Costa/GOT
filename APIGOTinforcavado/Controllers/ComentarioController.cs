using Microsoft.AspNetCore.Mvc;
using APIGOTinforcavado.Services;
using Shared.models;

namespace APIGOTinforcavado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly ComentarioService _comentarioService;

        public ComentarioController(ComentarioService comentarioService)
        {
            _comentarioService = comentarioService;
        }

        // Criar comentário
        [HttpPost]
        public async Task<IActionResult> CreateComentario([FromBody] Comentario comentario)
        {
            if (comentario == null)
                return BadRequest("Dados do comentário inválidos.");

            var createdComentario = await _comentarioService.CreateComentarioAsync(comentario);
            return CreatedAtAction(nameof(GetComentarioById), new { id = createdComentario.id }, createdComentario);
        }

        // Buscar comentário por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComentarioById(int id)
        {
            var comentario = await _comentarioService.GetComentarioByIdAsync(id);
            if (comentario == null)
                return NotFound();

            return Ok(comentario);
        }

        // Buscar todos os comentários
        [HttpGet]
        public async Task<IActionResult> GetAllComentarios()
        {
            var comentarios = await _comentarioService.GetAllComentariosAsync();
            return Ok(comentarios);
        }

        // Buscar comentários de um ticket específico
        [HttpGet("por-ticket/{ticketId}")]
        public async Task<IActionResult> GetComentariosByTicketId(int ticketId)
        {
            var comentarios = await _comentarioService.GetComentariosByTicketIdAsync(ticketId);
            return Ok(comentarios);
        }

        // Remover um comentário
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            var success = await _comentarioService.DeleteComentarioAsync(id);
            if (!success)
                return NotFound("Comentário não encontrado.");

            return NoContent();
        }
    }
}
