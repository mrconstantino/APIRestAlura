using Microsoft.EntityFrameworkCore;

namespace APIRestAlura.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Receitas> Receitas { get; set; }
    }
}
