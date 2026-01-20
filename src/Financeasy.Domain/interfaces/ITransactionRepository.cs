using System.Linq.Expressions;
using Financeasy.Domain.DTO.Transaction;
using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        public Task<Transaction?> GetTransactionWithCategoryAndBank(Guid transactionId);
        public Task<GetPagedTransResponseDTO> GetPagedWithRelationsAsync(    
            Expression<Func<Transaction, bool>> predicate,
            Expression<Func<Transaction, object>> orderBy,
            bool ascending,
            int page,
            int pageSize);
    }
}