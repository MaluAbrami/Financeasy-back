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
        public DbSet<BankAccount> BankAccount { get; set ;}
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(x => x.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Category>()
                .Property(x => x.RecurrenceType)
                .HasConversion<string>();

            modelBuilder.Entity<Category>()
                .HasIndex(e => new { e.UserId, e.Name })
                .IsUnique();

            modelBuilder.Entity<Card>()
                .Property(x => x.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Transaction>()
                .Property(x => x.PaymentMethod)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}