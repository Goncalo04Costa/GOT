using APIGOTinforcavado.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace APIGOTinforcavado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizadorController : ControllerBase
    {
        private readonly UtilizadorService _utilizadorService;

        public UtilizadorController(UtilizadorService utilizadorService)
        {
            _utilizadorService = utilizadorService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { Message = "Email e password são obrigatórios." });
            }

            var token = await _utilizadorService.AutenticarAsync(request.Email, request.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { Message = "Credenciais inválidas." });
            }

            return Ok(new { Token = token });
        }

        // Obter comentário por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUtilizadorById(int id)
        {
            var utilizador = await _utilizadorService.GetUtilizadorByIdAsync(id);
            if (utilizador == null)
                return NotFound();

            return Ok(utilizador);
        }
    }
    public class LoginRequest
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A password é obrigatória.")]
        public string Password { get; set; }
    }
}
