using Financeasy.Domain.interfaces;
using Financeasy.Infra.Persistence;

namespace Financeasy.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinanceasyDbContext _context;

        public UnitOfWork(FinanceasyDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}