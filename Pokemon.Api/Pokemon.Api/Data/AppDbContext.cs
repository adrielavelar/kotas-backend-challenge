using Microsoft.EntityFrameworkCore;
using Pokemon.Api.Models;

namespace Pokemon.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Capture> Captures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=pokemon.db");
            }
        }
    }
}
