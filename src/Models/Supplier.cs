namespace ProjControleEstoque.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string? Endereco { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public DateTime? Criado { get; set; }
        public int CriadoPorId { get; set; }

        public List<Product> Products { get; set; }
        public User? CriadoPor { get; set; }
    }
}
