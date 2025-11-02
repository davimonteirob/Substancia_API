using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SubstanciasAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PropriedadeController : ControllerBase
    {
        private readonly SubstanciasDbContext _context;

        public PropriedadeController(SubstanciasDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PropriedadeCreateDto dto)
        {
            var prop = new Propriedade { Nome = dto.Nome };
            _context.Propriedades.Add(prop);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObterPorId), new { id = prop.Id }, prop);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var propriedades = await _context.Propriedades.ToListAsync();
            return Ok(propriedades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var propriedade = await _context.Propriedades.FindAsync(id);
            if (propriedade == null) return NotFound();
            return Ok(propriedade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Propriedade propriedadeAtualizada)
        {
            var propriedade = await _context.Propriedades.FindAsync(id);
            if (propriedade == null) return NotFound();

            propriedade.Nome = propriedadeAtualizada.Nome;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var propriedade = await _context.Propriedades.FindAsync(id);
            if (propriedade == null) return NotFound();

            _context.Propriedades.Remove(propriedade);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
