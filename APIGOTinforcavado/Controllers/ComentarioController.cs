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

        [HttpPost]
        public async Task<IActionResult> CriarComentario([FromBody] ComentarioTicket comentario)
        {
            if (comentario == null)
            {
                return BadRequest("Comentário inválido.");
            }
            var resultado = await _comentarioService.CreateComentarioAsync(comentario);

            if (resultado == null)
            {
                return BadRequest("Erro ao criar o comentário.");
            }

            return CreatedAtAction(nameof(GetComentarioById), new { id = resultado.Id }, resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComentarioById(int id)
        {
            var comentario = await _comentarioService.GetComentarioByIdAsync(id);
            if (comentario == null)
                return NotFound();

            return Ok(comentario);
        }

  
        [HttpGet]
        public async Task<IActionResult> GetComentarios()
        {
            var comentarios = await _comentarioService.GetComentariosAsync();
            return Ok(comentarios);
        }


        [HttpGet("ticket/{codigo}")]
        public async Task<IActionResult> GetComentariosPorCodigoTicket(string codigo)
        {
            var comentarios = await _comentarioService.GetComentariosPorCodigoTicketAsync(codigo);

            if (comentarios == null || comentarios.Count == 0)
                return NotFound();

            return Ok(comentarios);
        }


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
