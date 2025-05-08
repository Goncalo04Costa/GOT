using APIGOTinforcavado.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Shared.models;
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

        // GET: api/utilizador/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUtilizadorById(int id)
        {
            var utilizador = await _utilizadorService.GetUtilizadorByIdAsync(id);
            if (utilizador == null)
                return NotFound();

            return Ok(utilizador);
        }

        // GET: api/utilizador
        [HttpGet]
        public async Task<IActionResult> GetUtilizadores()
        {
            var utilizadores = await _utilizadorService.GetUtilizadoresAsync();
            return Ok(utilizadores);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoUtilizador = new Utilizador
            {
                Email = request.Email,
                Password = request.Password, // Será hash no Service
                Nome = request.Nome
            };

            var criado = await _utilizadorService.CreateUtilizadorAsync(novoUtilizador);
            return CreatedAtAction(nameof(GetUtilizadorById), new { id = criado.Id }, criado);
        }

        // POST: api/utilizador/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _utilizadorService.LoginAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}


    public class RegisterRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A password é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Password { get; set; }
    }


