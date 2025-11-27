using Financeasy.Domain.Models;

namespace Financeasy.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        public Task<Transaction> GetTransactionByIdWithCategory(Guid id);
        public Task<List<Transaction>> GetTransactionsWithCategory(Guid userId);
    }
}