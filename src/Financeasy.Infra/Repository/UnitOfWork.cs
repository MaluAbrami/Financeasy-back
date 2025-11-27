using Financeasy.Domain.Interfaces;
using Financeasy.Infra.Data;

namespace Financeasy.Infra.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinanceasyDbContext _dbContext;

        public UnitOfWork(FinanceasyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}