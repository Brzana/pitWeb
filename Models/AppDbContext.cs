using Microsoft.EntityFrameworkCore;

namespace rozliczeniaPIT.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<RozliczenieModel> Rozliczenia { get; set; }
    }
}