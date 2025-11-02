using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubstanciasAPI.DTOs;


namespace SubstanciasAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SubstanciaController : ControllerBase
    {
        private readonly SubstanciasDbContext _context;
        private readonly CryptoService _crypto;

        public SubstanciaController(SubstanciasDbContext context, CryptoService crypto)
        {
            _context = context;
            _crypto = crypto;
        }


        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] SubstanciaCreateDto dto)
        {

            var substancia = new Substancia
            {
                Nome = _crypto.Encrypt(dto.Nome),
                Codigo = dto.Codigo,
                Descricao = _crypto.Encrypt(dto.Descricao),
                Notas = _crypto.Encrypt(dto.Notas),
                CategoriaId = dto.CategoriaId
            };

            _context.Substancias.Add(substancia);
            await _context.SaveChangesAsync();


            if (dto.Propriedades != null)
            {
                foreach (var sp in dto.Propriedades)
                {
                    _context.SubstanciaPropriedades.Add(new SubstanciaPropriedade
                    {
                        SubstanciaId = substancia.Id, 
                        PropriedadeId = sp.PropriedadeId,
                        ValorBool = sp.ValorBool,
                        ValorDecimal = sp.ValorDecimal
                    });
                }
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(ObterPorId), new { id = substancia.Id }, substancia.Id);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var lista = await _context.Substancias
                .Include(s => s.Categoria)
                .Include(s => s.Propriedades)
                    .ThenInclude(sp => sp.Propriedade)
                .ToListAsync();

            var listaDto = lista.Select(s => new SubstanciaReadDto
            {
                Id = s.Id,
                Nome = _crypto.Decrypt(s.Nome),
                Codigo = s.Codigo,
                Descricao = _crypto.Decrypt(s.Descricao),
                Notas = _crypto.Decrypt(s.Notas),
                Categoria = new CategoriaReadDto
                {
                    Id = s.Categoria.Id,
                    Nome = s.Categoria.Nome
                },
                Propriedades = s.Propriedades.Select(sp => new SubstanciaPropriedadeReadDto
                {
                    PropriedadeId = sp.PropriedadeId,
                    NomePropriedade = sp.Propriedade.Nome,
                    ValorBool = sp.ValorBool,
                    ValorDecimal = sp.ValorDecimal
                }).ToList()
            }).ToList();

            return Ok(listaDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var substancia = await _context.Substancias
                .Include(s => s.Categoria)
                .Include(s => s.Propriedades)
                .ThenInclude(sp => sp.Propriedade)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (substancia == null) return NotFound();

            substancia.Nome = _crypto.Decrypt(substancia.Nome);
            substancia.Descricao = _crypto.Decrypt(substancia.Descricao);
            substancia.Notas = _crypto.Decrypt(substancia.Notas);


            var substanciaDto = new SubstanciaReadDto
            {
                Id = substancia.Id,
                Nome = substancia.Nome,
                Codigo = substancia.Codigo,
                Descricao = substancia.Descricao,
                Notas = substancia.Notas,
                Categoria = new CategoriaReadDto
                {
                    Id = substancia.Categoria.Id,
                    Nome = substancia.Categoria.Nome
                },
                Propriedades = substancia.Propriedades.Select(sp => new SubstanciaPropriedadeReadDto
                {
                    PropriedadeId = sp.PropriedadeId,
                    NomePropriedade = sp.Propriedade.Nome,
                    ValorBool = sp.ValorBool,
                    ValorDecimal = sp.ValorDecimal
                }).ToList()
            };

            return Ok(substanciaDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, SubstanciaUpdateDto substanciaDto)
        {
            var substancia = await _context.Substancias
                .Include(s => s.Propriedades)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (substancia == null) return NotFound();

            substancia.Nome = _crypto.Encrypt(substanciaDto.Nome);
            substancia.Descricao = _crypto.Encrypt(substanciaDto.Descricao);
            substancia.Notas = _crypto.Encrypt(substanciaDto.Notas);
            substancia.CategoriaId = substanciaDto.CategoriaId;

            if (substanciaDto.Propriedades != null)
            {
                _context.SubstanciaPropriedades.RemoveRange(substancia.Propriedades);

                foreach (var spDto in substanciaDto.Propriedades)
                {
                    var sp = new SubstanciaPropriedade
                    {
                        SubstanciaId = id,
                        PropriedadeId = spDto.PropriedadeId,
                        ValorBool = spDto.ValorBool,
                        ValorDecimal = spDto.ValorDecimal
                    };
                    _context.SubstanciaPropriedades.Add(sp);
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var substancia = await _context.Substancias.FindAsync(id);
            if (substancia == null) return NotFound();

            _context.Substancias.Remove(substancia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
