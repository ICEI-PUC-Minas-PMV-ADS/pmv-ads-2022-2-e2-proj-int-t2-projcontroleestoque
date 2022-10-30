namespace ProjControleEstoque.Models
{
    public class User
    { 
           
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Hash { get; set; }
        public string? Funcao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string? Email { get; set; }

    }
}
