using CedroRestaurante.DataObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace CedroRestaurante.Persistence.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Restaurante> Restaurate { get; set; }
        public DbSet<Prato> Prato { get; set; }
    }
}
