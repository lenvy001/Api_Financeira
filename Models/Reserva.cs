namespace api_financeiro.Models
{
    public class Reserva
    {
       public string Id { get; set; } = Guid.NewGuid().ToString();
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public  DateOnly Data { get; set; }   /*marca data reserva entre semana e mes*/

        public int Mes => Data.Month; /*marca o mes da reserva para calcular o total mensal*/
        public int Ano => Data.Year; /*marca o ano da reserva para calcular o total anual*/

    }
}
