using api_financeiro.Data;
using api_financeiro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_financeiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GanhoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GanhoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var ganhos = await _context.Ganhos.ToListAsync();
            return Ok(ganhos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var ganho = await _context.Ganhos.FindAsync(id);
            if (ganho is null)
                return NotFound("Ganho não encontrado.");
            return Ok(ganho);
        }

        [HttpGet("mes/{mes}/ano/{ano}")]
        public async Task<IActionResult> GetPorMesEAno(int mes, int ano)
        {
            var dataInicio = new DateOnly(ano, mes, 1);
            var dataFim = dataInicio.AddMonths(1).AddDays(-1);

            var filtrados = await _context.Ganhos
                .Where(g => g.Data >= dataInicio && g.Data <= dataFim)
                .ToListAsync();

            if (!filtrados.Any())
                return NotFound($"Nenhum ganho encontrado para {mes}/{ano}.");

            return Ok(filtrados);
        }

        [HttpGet("ano/{ano}")]
        public async Task<IActionResult> GetPorAno(int ano)
        {
            var dataInicio = new DateOnly(ano, 1, 1);
            var dataFim = new DateOnly(ano, 12, 31);

            var filtrados = await _context.Ganhos
                .Where(g => g.Data >= dataInicio && g.Data <= dataFim)
                .ToListAsync();

            if (!filtrados.Any())
                return NotFound($"Nenhum ganho encontrado para o ano {ano}.");

            return Ok(filtrados);
        }

        [HttpPost]
        public async Task<IActionResult> Cria([FromBody] Ganho ganho)
        {
            if (ganho == null || ganho.Valor <= 0)
                return BadRequest("Ganho inválido. O valor deve ser maior que zero.");

            ganho.Id = Guid.NewGuid().ToString(); // servidor controla o ID
            _context.Ganhos.Add(ganho);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = ganho.Id }, ganho);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleta(string id)
        {
            var ganho = await _context.Ganhos.FindAsync(id);
            if (ganho is null)
                return NotFound("Ganho não encontrado.");

            _context.Ganhos.Remove(ganho);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualiza(string id, [FromBody] Ganho ganhoAtualizado)
        {
            var ganho = await _context.Ganhos.FindAsync(id);
            if (ganho is null)
                return NotFound("Ganho não encontrado.");

            if (ganhoAtualizado == null || ganhoAtualizado.Valor <= 0)
                return BadRequest("Ganho inválido. O valor deve ser maior que zero.");

            ganho.Valor = ganhoAtualizado.Valor;
            ganho.Data = ganhoAtualizado.Data;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}