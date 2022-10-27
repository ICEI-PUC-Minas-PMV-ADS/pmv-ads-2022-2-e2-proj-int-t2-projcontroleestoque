namespace ProjControleEstoque.Data.bodies
{
    public class ProductForm
    {
        public int? Id { get; set; }
        public string? nome { get; set; }
        public string? descricao { get; set; }
        public int? quantidade { get; set; }
        public int? fornecedorId { get; set; }
        public string? localizacao { get; set; }
        public string? tags { get; set; }
        public string? estrategiaConsumo { get; set; }
        public string? validade { get; set; }
    }
}
