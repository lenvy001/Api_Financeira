using api_financeiro.Data;
using api_financeiro.Models;
using Microsoft.AspNetCore.Mvc;

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

        /* Método para obter todos os ganhos. Retorna a lista completa de ganhos armazenados.*/

        [HttpGet]
        public IActionResult GetTodos()
        {
            var ganhos = _context.Ganhos.ToList();
            return Ok(ganhos);
        }


        [HttpGet("mes/{mes}/ano/{ano}")]
        public IActionResult GetPorMesEAno(int mes, int ano)
        {
            var filtrados = _context.Ganhos
                .ToList() // traz pro C# primeiro
                .Where(g => g.Mes == mes && g.Ano == ano)
                .ToList();

            if (!filtrados.Any())
                return NotFound($"Nenhum ganho encontrado para {mes}/{ano}.");

            return Ok(filtrados);
        }

        [HttpGet("ano/{ano}")]
        public IActionResult GetPorAno(int ano)
        {
            var filtrados = _context.Ganhos
                .ToList()
                .Where(g => g.Ano == ano)
                .ToList();

            if (!filtrados.Any())
                return NotFound($"Nenhum ganho encontrado para o ano {ano}.");

            return Ok(filtrados);
        }
        /** Método para criar um novo ganho. Recebe um objeto Ganho no corpo da requisição e o adiciona à lista de ganhos.*/

        [HttpPost]
        public IActionResult Cria([FromBody] Ganho ganho)
        {
            if (ganho == null || ganho.Valor <= 0)
            {
                return BadRequest("Ganho inválido. O valor deve ser maior que zero.");
            }
            _context.Ganhos.Add(ganho);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTodos), new { id = ganho.Id }, ganho);
        }
        /* Método para deletar um ganho. Recebe o ID do ganho a ser deletado e remove-o da lista de ganhos. Retorna NotFound se o ganho não for encontrado.*/

        [HttpDelete("{id}")]
        public IActionResult Deleta(string id)
        {
            var ganho = _context.Ganhos.FirstOrDefault(g => g.Id == id);
            if (ganho == null)
            {
                return NotFound("Ganho não encontrado.");
            }
            _context.Ganhos.Remove(ganho);
            _context.SaveChanges();
            return NoContent();
        }
        /* Método para atualizar um ganho. Recebe o ID do ganho a ser atualizado e um objeto Ganho com os dados atualizados. Retorna NotFound se o ganho não for encontrado ou BadRequest se os dados forem inválidos.*/

        [HttpPut("{id}")]
        public IActionResult Atualiza(string id, [FromBody] Ganho ganhoAtualizado)
        {
            var ganho = _context.Ganhos.FirstOrDefault(g => g.Id == id);

            if (ganho == null)
            {
                return NotFound("Ganho não encontrado.");
            }

            if (ganhoAtualizado == null || ganhoAtualizado.Valor <= 0)
            {
                return BadRequest("Ganho inválido. O valor deve ser maior que zero.");
            }

            ganho.Valor = ganhoAtualizado.Valor;
            ganho.Data = ganhoAtualizado.Data;  
            _context.SaveChanges();

            return NoContent();
        }
    }
} /* finalizo o controller de ganho, com metodos get,post,delete,put -1 dor de cabeça enquanto nao der erro aqui */