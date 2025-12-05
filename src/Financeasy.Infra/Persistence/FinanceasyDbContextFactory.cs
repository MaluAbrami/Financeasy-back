using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Financeasy.Infra.Persistence
{
    public class FinanceasyDbContextFactory : IDesignTimeDbContextFactory<FinanceasyDbContext>
    {
        public FinanceasyDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Financeasy.Api");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.Development.json", optional: false)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<FinanceasyDbContext>();
            optionsBuilder.UseMySQL(connectionString!);

            return new FinanceasyDbContext(optionsBuilder.Options);
        }
    }
}
