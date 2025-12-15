using Financeasy.Domain.models;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.Persistence
{
    public class FinanceasyDbContext : DbContext
    {
        public FinanceasyDbContext(DbContextOptions<FinanceasyDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FinancialEntry> FinancialEntry { get; set ;}
        public DbSet<Category> Categorys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração para salvar o enum Category como string
            modelBuilder.Entity<FinancialEntry>()
                .Property(x => x.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Category>()
                .HasIndex(e => new { e.UserId, e.Name })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}