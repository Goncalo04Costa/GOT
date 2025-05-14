using APIGOTinforcavado.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIGOTinforcavado.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly UtilizadorService _utilizadorService;

        public EmpresaController(UtilizadorService utilizadorService)
        {
            _utilizadorService = utilizadorService;
        }


        // GET: api/empresa/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpresaById(int id)
        {
            var empresa = await _utilizadorService.GetEmpresaByIdAsync(id);
            if (empresa == null)
                return NotFound();

            return Ok(empresa);
        }

    }
}
