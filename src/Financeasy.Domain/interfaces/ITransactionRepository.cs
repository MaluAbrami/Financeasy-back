using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        public Task<Transaction?> GetTransactionWithCategoryAndBank(Guid transactionId);
    }
}