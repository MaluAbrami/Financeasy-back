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
        public DbSet<RecurrenceRule> RecurrenceRules { get; set; }
        public DbSet<Update> Updates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinancialEntry>()
                .Property(x => x.Type)
                .HasConversion<string>();

            modelBuilder.Entity<FinancialEntry>()
                .Property(x => x.Source)
                .HasConversion<string>();

            modelBuilder.Entity<Category>()
                .Property(x => x.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Category>()
                .HasIndex(e => new { e.UserId, e.Name })
                .IsUnique();

            modelBuilder.Entity<RecurrenceRule>()
                .Property(x => x.AdjustmentRule)
                .HasConversion<string>();

            modelBuilder.Entity<RecurrenceRule>()
                .Property(x => x.Frequency)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}