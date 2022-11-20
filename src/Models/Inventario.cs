namespace ProjControleEstoque.Models
{
    public class Inventario
    {
        public int Id { get; set; }
        public int RealizadoPorId { get; set; }
        public int SolicitadoPorId { get; set; }
        public DateTime? DataDeExecucao { get; set; }
        public DateTime? DataCriacao { get; set; }

        public User? RealizadoPor { get; set; }
        public User? SolicitadoPor { get; set; }
        public List<ItemInventario>? ItemsInventario { get; set; }
    }
}
