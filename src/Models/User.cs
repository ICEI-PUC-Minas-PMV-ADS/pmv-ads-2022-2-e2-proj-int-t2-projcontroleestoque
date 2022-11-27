using System.ComponentModel;

namespace ProjControleEstoque.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Hash { get; set; }

        [DisplayName("Função")]
        public string? Funcao { get; set; }

        [DisplayName("Data de cadastro")]
        public DateTime? DataCadastro { get; set; }
        public string? Email { get; set; }

    }
    
}
