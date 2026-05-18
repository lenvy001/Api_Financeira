namespace api_financeiro.Models
{
    public class Resumo
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
       public  decimal ValorGanho { get; set; }
        public decimal ValorGasto { get; set; }
        
        public decimal ValorReserva { get; set; }

        public decimal Saldo => ValorGanho - ValorGasto - ValorReserva;

    }
}
