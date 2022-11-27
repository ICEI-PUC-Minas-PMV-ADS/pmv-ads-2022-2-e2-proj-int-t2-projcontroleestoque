using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ProjControleEstoque.Models;

namespace ProjControleEstoque.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Fornecedor)
                .WithMany(b => b.Products);

            modelBuilder.Entity<Supplier>()
                .HasOne(p => p.CriadoPor);

            modelBuilder.Entity<MovimentacaoEstoque>()
                .HasOne(p => p.RegistradoPor);

            modelBuilder.Entity<MovimentacaoEstoque>()
                .HasOne(p => p.SolicitadoPor);

            modelBuilder.Entity<MovimentacaoEstoque>()
                .HasOne(m => m.Produto);
        }

        public DbSet<Product>? Products { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Supplier>? Suppliers { get; set; }
        public DbSet<MovimentacaoEstoque>? movimentacaoEstoques { get; set; }
        public DbSet<Inventario>? Inventarios { get; set; }
        public DbSet<ItemInventario>? ItemInventarios { get; set; }
        public DbSet<AgendaInventario> AgendaInventarios { get; set; }
    }
}
