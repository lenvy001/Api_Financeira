using api_financeiro.Data;
using api_financeiro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api_financeiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservaController(AppDbContext context)
            {
                _context = context;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            var reservas = _context.Reservas.ToList();
            if (!reservas.Any())
                return NotFound("Nenhuma reserva encontrada.");
            return Ok(reservas);
        }

        [HttpGet("mes/{mes}/ano/{ano}")]
        public async Task<IActionResult> GetPorMesEAno(int mes, int ano)
        {
            if (mes < 1 || mes > 12)
                return BadRequest("Mês inválido.");

            var dataInicio = new DateOnly(ano, mes, 1);
            var dataFim = dataInicio.AddMonths(1).AddDays(-1);

            var filtrados = await _context.Reservas
                .Where(r => r.Data >= dataInicio && r.Data <= dataFim)
                .ToListAsync();

            if (!filtrados.Any())
                return NotFound($"Nenhuma reserva encontrada para {mes}/{ano}.");

            return Ok(filtrados);
        }

        [HttpGet("ano/{ano}")]
        public async Task<IActionResult> GetPorAno(int ano)
        {
            var dataInicio = new DateOnly(ano, 1, 1);
            var dataFim = new DateOnly(ano, 12, 31);

            var filtrados = await _context.Reservas
                .Where(r => r.Data >= dataInicio && r.Data <= dataFim)
                .ToListAsync();

            if (!filtrados.Any())
                return NotFound($"Nenhuma reserva encontrada para o ano {ano}.");

            return Ok(filtrados);
        }

        [HttpPost]
        public IActionResult Cria([FromBody] Reserva reserva)
        {
            if (reserva == null || reserva.Valor <= 0)
                return BadRequest("Reserva inválida. O valor deve ser maior que zero.");

            _context.Reservas.Add(reserva);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTodos), new { id = reserva.Id }, reserva);
        }

        [HttpDelete("{id}")]
        public IActionResult Deleta(string id)
        {
            var reserva = _context.Reservas.FirstOrDefault(r => r.Id == id);
            if (reserva == null)
                return NotFound("Reserva não encontrada.");

            _context.Reservas.Remove(reserva);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Atualiza(string id, [FromBody] Reserva reservaAtualizada)
        {
            var reserva = _context.Reservas.FirstOrDefault(r => r.Id == id);
            if (reserva == null)
                return NotFound("Reserva não encontrada.");

            if (reservaAtualizada == null || reservaAtualizada.Valor <= 0)
                return BadRequest("Reserva inválida. O valor deve ser maior que zero.");

            reserva.Descricao = reservaAtualizada.Descricao;
            reserva.Valor = reservaAtualizada.Valor;
            reserva.Data = reservaAtualizada.Data;
            
            _context.SaveChanges();


            return NoContent();
        }
    }
}