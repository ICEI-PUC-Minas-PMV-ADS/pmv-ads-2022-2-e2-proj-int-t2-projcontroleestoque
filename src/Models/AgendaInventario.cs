using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjControleEstoque.Models
{
    public class AgendaInventario
    {
        public int Id { get; set; }
        public int? InventarioId { get; set; }
        public string? Tipo { get; set; }
        public string? Agendamento { get; set; }
        public int? SolicitadoPorId { get; set; }

        public Inventario? Inventario { get; set; }
        public User? SolicitadoPor { get; set; }
    }
}
