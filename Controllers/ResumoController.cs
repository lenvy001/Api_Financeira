using api_financeiro.Data;
using api_financeiro.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetResumoGeral()
        {
            var totalGanhos = _context.Ganhos
                .ToList()
                .Sum(g => g.Valor);

            var totalGastos = _context.Gastos
                .ToList()
                .Sum(g => g.Valor);

            var totalReservas = _context.Reservas
                .ToList()
                .Sum(r => r.Valor);

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