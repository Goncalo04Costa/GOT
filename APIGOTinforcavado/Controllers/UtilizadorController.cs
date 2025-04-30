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

     


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUtilizadorById(int id)
        {
            var utilizador = await _utilizadorService.GetUtilizadorByIdAsync(id);
            if (utilizador == null)
                return NotFound();

            return Ok(utilizador);
        }

        [HttpGet]
        public async Task<IActionResult> GetUtilizadores()
        {
            var utilizadores = await _utilizadorService.GetUtilizadoresAsync();
            return Ok(utilizadores);
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
