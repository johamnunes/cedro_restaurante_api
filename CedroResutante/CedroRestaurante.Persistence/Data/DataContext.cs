using CedroRestaurante.DataObjects;
using Microsoft.EntityFrameworkCore;

namespace CedroRestaurante.Persistence.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Restaurante> Restaurate { get; set; }
        public DbSet<Prato> Prato { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurante>().Ignore(m => m.Removido);
            modelBuilder.Entity<Prato>().Ignore(m => m.Removido);
        }
    }
}
