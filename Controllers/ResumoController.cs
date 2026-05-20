using api_financeiro.Data;
using api_financeiro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_financeiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ResumoController(AppDbContext context)
        {
            _context = context;
    }

        // GET api/resumo → resumo geral de tudo
        [HttpGet]
        public async Task<IActionResult> GetResumoGeral()
        {
            var totalGanhos = await _context.Ganhos.SumAsync(g => g.Valor);
            var totalGastos = await _context.Gastos.SumAsync(g => g.Valor);
            var totalReservas = await _context.Reservas.SumAsync(r => r.Valor);

            return Ok(new
            {
                totalGanhos,
                totalGastos,
                totalReservas,
                saldo = totalGanhos - totalGastos - totalReservas
            });
        }
    }
}