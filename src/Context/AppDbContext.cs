using Microsoft.EntityFrameworkCore;
using ProjControleEstoque.Models;

namespace ProjControleEstoque.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product>? Products { get; set; }
    }
}
