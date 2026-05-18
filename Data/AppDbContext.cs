using Microsoft.EntityFrameworkCore;
using api_financeiro.Models;

namespace api_financeiro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Ganho> Ganhos { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
       
    }
}