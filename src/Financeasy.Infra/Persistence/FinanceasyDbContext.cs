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
    }
}