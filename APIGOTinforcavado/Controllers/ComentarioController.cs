using Microsoft.AspNetCore.Mvc;
using APIGOTinforcavado.Services;
using Shared.models;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        // Criar novo Comentário
        [HttpPost]
        public async Task<IActionResult> CreateComentario([FromBody] Comentario comentario)
        {
            if (comentario == null)
                return BadRequest("Os dados do comentário são inválidos.");

            var createdComentario = await _comentarioService.CreateComentarioAsync(comentario);
            return CreatedAtAction(nameof(GetComentarioById), new { id = createdComentario.Id }, createdComentario);
        }

        // Obter comentário por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComentarioById(int id)
        {
            var comentario = await _comentarioService.GetComentarioByIdAsync(id);
            if (comentario == null)
                return NotFound();

            return Ok(comentario);
        }

        // Listar todos os comentários
        [HttpGet]
        public async Task<IActionResult> GetComentarios()
        {
            var comentarios = await _comentarioService.GetComentariosAsync();
            return Ok(comentarios);
        }

        // Atualizar comentário
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComentario(int id, [FromBody] Comentario comentario)
        {
            if (id != comentario.Id)
                return BadRequest("O ID do comentário não confere.");

            var updatedComentario = await _comentarioService.UpdateComentarioAsync(id, comentario);
            if (updatedComentario == null)
                return NotFound();

            return Ok(updatedComentario);
        }

     

        [HttpGet("ticket/{ticketId}")]
        public async Task<IActionResult> GetComentariosByTicketId(string ticketId)
        {
            var comentarios = await _comentarioService.GetComentariosByTicketIdAsync(ticketId);
            if (comentarios == null || !comentarios.Any())
                return NotFound(new { Message = $"Nenhum comentário encontrado para o TicketId {ticketId}." });

            return Ok(comentarios);
        }



        // Excluir comentário
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            var result = await _comentarioService.DeleteComentarioAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
