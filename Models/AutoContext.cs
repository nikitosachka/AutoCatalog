using Microsoft.EntityFrameworkCore;

namespace AutoCatalog.Models
{
    public class AutoContext : DbContext
    {
        public AutoContext(DbContextOptions<AutoContext> options) : base(options)
        {
        }

        public DbSet<Auto> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Admin> Admin { get; set; }

    }
}
