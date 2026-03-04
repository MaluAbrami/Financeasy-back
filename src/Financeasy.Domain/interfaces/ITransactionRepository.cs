using System.Linq.Expressions;
using Financeasy.Domain.DTO.Transaction;
using Financeasy.Domain.Enums;
using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        public Task<Transaction?> GetTransactionWithCategoryAndBank(Guid transactionId);
    }
}