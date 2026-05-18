namespace api_financeiro.Models
{
    public class Gasto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty; // opcional
        public DateOnly Data { get; set; }

        public int Mes => Data.Month;
        public int Ano => Data.Year;
    }
}   