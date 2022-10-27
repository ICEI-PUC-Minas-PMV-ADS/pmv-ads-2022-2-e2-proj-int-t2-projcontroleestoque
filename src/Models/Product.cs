namespace ProjControleEstoque.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int Quantidade { get; set; }
        public string? Localizacao { get; set; }
        public string? Tags { get; set; }
        public int? FornecedorId { get; set; }
        public string? EstrategiaConsumo { get; set; }
        public DateTime? Criado { get; set; }
        public DateTime? Validade { get; set; }

        public Supplier? Fornecedor { get; set; }
    }
}
