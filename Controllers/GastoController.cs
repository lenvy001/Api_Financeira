using api_financeiro.Data;
using api_financeiro.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_financeiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GastoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GastoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            return Ok(_context.Gastos.ToList());
        }

        [HttpGet("mes/{mes}/ano/{ano}")]
        public IActionResult GetPorMesEAno(int mes, int ano)
        {
            var filtrados = _context.Gastos
                .ToList()
                .Where(g => g.Mes == mes && g.Ano == ano)
                .ToList();

            if (!filtrados.Any())
                return NotFound($"Nenhum gasto encontrado para {mes}/{ano}.");

            return Ok(filtrados);
        }

        [HttpGet("ano/{ano}")]
        public IActionResult GetPorAno(int ano)
        {
            var filtrados = _context.Gastos
                .ToList()
                .Where(g => g.Ano == ano)
                .ToList();

            if (!filtrados.Any())
                return NotFound($"Nenhum gasto encontrado para o ano {ano}.");

            return Ok(filtrados);
        }

        [HttpPost]
        public IActionResult Cria([FromBody] Gasto novoGasto)
        {
            if (novoGasto == null || novoGasto.Valor <= 0)
                return BadRequest("Gasto inválido. O valor deve ser maior que zero.");

            _context.Gastos.Add(novoGasto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTodos), new { id = novoGasto.Id }, novoGasto);
        }

        [HttpDelete("{id}")]
        public IActionResult Deleta(string id)
        {
            var gasto = _context.Gastos.FirstOrDefault(g => g.Id == id);
            if (gasto == null)
                return NotFound("Gasto não encontrado.");

            _context.Gastos.Remove(gasto);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Atualiza(string id, [FromBody] Gasto gastoAtualizado)
        {
            var gasto = _context.Gastos.FirstOrDefault(g => g.Id == id);
            if (gasto == null)
                return NotFound("Gasto não encontrado.");

            if (gastoAtualizado == null || gastoAtualizado.Valor <= 0)
                return BadRequest("Gasto inválido. O valor deve ser maior que zero.");

            gasto.Valor = gastoAtualizado.Valor;
            gasto.Data = gastoAtualizado.Data;
            _context.SaveChanges();
            return NoContent();
        }
    }
}