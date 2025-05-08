using Microsoft.AspNetCore.Mvc;
using APIGOTinforcavado.Services;
using Shared.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIGOTinforcavado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotMessagesController : ControllerBase
    {
        private readonly ChatBotMessagesService _service;

        public ChatBotMessagesController(ChatBotMessagesService service)
        {
            _service = service;
        }

        
        [HttpPost]
        public async Task<IActionResult> CriarMensagem([FromBody] ChatBotMessages mensagem)
        {
            if (mensagem == null)
                return BadRequest("Mensagem inválida.");

            var resultado = await _service.CriarMensagemAsync(mensagem);

            return CreatedAtAction(nameof(GetMensagemByPalavra), new { texto = mensagem.PalavrasChave }, resultado);
        }


        [HttpPost("responder")]
        public async Task<IActionResult> ObterResposta([FromBody] string mensagemUsuario)
        {
            if (string.IsNullOrWhiteSpace(mensagemUsuario))
                return BadRequest("Mensagem inválida.");

            var resposta = await _service.ObterMensagemPorPalavrasChaveAsync(mensagemUsuario);

            if (string.IsNullOrEmpty(resposta))
                return NotFound("Nenhuma resposta encontrada.");

            return Ok(new { resposta });
        }

        [HttpGet]
        public async Task<IActionResult> GetMensagens()
        {
            var mensagens = await _service.ObterTodasMensagensAsync();
            return Ok(mensagens);
        }

       
        [HttpGet("buscar")]
        public async Task<IActionResult> GetMensagemByPalavra([FromQuery] string texto)
        {
            var resposta = await _service.ObterMensagemPorPalavrasChaveAsync(texto);

            if (resposta == null)
                return NotFound("Nenhuma resposta encontrada.");

            return Ok(resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMensagem(int id)
        {
            var result = await _service.ApagarMensagemAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
