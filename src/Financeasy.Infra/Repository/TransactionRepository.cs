
using Financeasy.Domain.Interfaces;
using Financeasy.Domain.Models;
using Financeasy.Infra.Data;

namespace Financeasy.Infra.Repository
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(FinanceasyDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Transaction> GetTransactionByIdWithCategory(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Transaction>> GetTransactionsWithCategory(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}