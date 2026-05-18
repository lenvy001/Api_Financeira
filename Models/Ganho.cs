namespace api_financeiro.Models
{
    public class Ganho
    {
       public  string Id { get; set; } = Guid.NewGuid().ToString(); /*gera um id unico para cada ganho*/
        public decimal Valor { get; set; }
        public DateOnly Data { get; set; }   /*marca data ganho entre semana e mes*/
       
        public int Mes => Data.Month; /*marca o mes do ganho para calcular o total mensal*/
        public int Ano => Data.Year; /*marca o ano do ganho para calcular o total anual*/
    }
}
