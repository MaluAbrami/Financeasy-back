using System;
using Financeasy.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.Data
{
    public class FinanceasyDbContext : DbContext
    {
        public FinanceasyDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<MonthlySummary> MonthlySummaries { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<RecurringTransaction> RecurringTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicar todas as configurações definidas por DataAnnotations automaticamente
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);

            // RELACIONAMENTOS ESPECÍFICOS QUE PRECISAM SER FORÇADOS

            // 1:1 -> User ↔ UserSettings
            modelBuilder.Entity<User>()
                .HasOne(u => u.Settings)
                .WithOne(s => s.User)
                .HasForeignKey<UserSettings>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1:N -> User → Categories
            modelBuilder.Entity<User>()
                .HasMany<Category>()
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1:N -> Category → Transactions
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Transactions)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1:N → User → Transactions
            modelBuilder.Entity<User>()
                .HasMany<Transaction>()
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Optional relationship: Transaction → RecurringTransaction
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.RecurringTransaction)
                .WithMany(r => r.Transactions)
                .HasForeignKey(t => t.RecurrenceId)
                .OnDelete(DeleteBehavior.SetNull);

            // CalendarEvent → Transaction (opcional)
            modelBuilder.Entity<CalendarEvent>()
                .HasOne(c => c.Transaction)
                .WithMany()
                .HasForeignKey(c => c.TransactionId)
                .OnDelete(DeleteBehavior.SetNull);

            // JSONB: garante que json seja jsonb
            modelBuilder
                .Entity<MonthlySummary>()
                .Property(m => m.CategoryBreakdownJson)
                .HasColumnType("jsonb");
        }
    }
}