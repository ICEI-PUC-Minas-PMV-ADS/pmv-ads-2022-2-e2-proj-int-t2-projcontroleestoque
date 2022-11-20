namespace ProjControleEstoque.Models
{
    public class MovimentacaoEstoque
    {
        public int Id { get; set; }
        public string? Motivo { get; set; }
        public string? Tipo { get; set; }
        public int Quantidade { get; set; }
        public int RegistradoPorId { get; set; }
<<<<<<< Updated upstream
        public int SolicitadoPorId { get; set; }
=======
        public int? SolicitadoPorId { get; set; }
>>>>>>> Stashed changes
        public int ProdutoId { get; set; }
        public DateTime? DataMovimento { get; set; }

        public User? RegistradoPor { get; set; }
        public User? SolicitadoPor { get; set; }
        public Product? Produto { get; set; }

    }
}
