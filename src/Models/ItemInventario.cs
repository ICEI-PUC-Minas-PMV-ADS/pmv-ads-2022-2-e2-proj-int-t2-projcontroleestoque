namespace ProjControleEstoque.Models
{
    public class ItemInventario
    {
        public int Id { get; set; }
        public int InventarioId { get; set; }
        public int ProdutoId { get; set; }
        public string? Observacao { get; set; }
        public string? Status { get; set; }
        public int Quantidade { get; set; }

        public Inventario? Inventario { get; set; }
        public Product? Produto { get; set; }
    }
}
