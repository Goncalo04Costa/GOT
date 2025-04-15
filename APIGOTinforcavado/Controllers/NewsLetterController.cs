using Microsoft.AspNetCore.Mvc;
using Shared.models;
using APIGOTinforcavado.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIGOTinforcavado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsLetterController : ControllerBase
    {
        private readonly NewsLetterService _service;

        public NewsLetterController(NewsLetterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<NewsLetter>>> GetAll()
        {
            var emails = await _service.GetAllAsync();
            return Ok(emails);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NewsLetter newsLetter)
        {
            await _service.AddAsync(newsLetter);
            return Ok(newsLetter);
        }

        [HttpDelete("{email}")]
        public async Task<ActionResult> Delete(string email)
        {
            await _service.DeleteAsync(email);
            return NoContent();
        }
    }
}
