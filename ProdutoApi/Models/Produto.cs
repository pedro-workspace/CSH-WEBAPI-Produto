namespace ProdutoApi.Models
{
    public class Produto
    {
        public int Id {get;set;}
        public string? Nome {get;set;}
        public double? Price {get;set;}
        public int? Estoque {get;set;}
    }
}