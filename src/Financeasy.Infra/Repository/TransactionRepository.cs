using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.Repository
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly FinanceasyDbContext _dbContext;

        public TransactionRepository(FinanceasyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Transaction?> GetTransactionWithCategoryAndBank(Guid transactionId)
        {
            return await _dbContext.Transactions
                .Include(t => t.Category)
                .Include(t => t.BankAccount)
                .FirstOrDefaultAsync(t => t.Id == transactionId);
        }
    }
}