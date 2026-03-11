using System.Transactions;
using Financeasy.Domain.DTO.Transaction;

namespace Financeasy.Domain.interfaces
{
    public interface ITransactionDapperRepository
    {
        public Task<GetPagedTransResponseDTO> GetPagedAsync(
            Guid userId,
            string? descriptionFilter,
            string orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken);

        public Task<decimal> GetTotalBalanceMonthlyIncome(
            Guid userId,
            int month,
            int year,
            CancellationToken cancellationToken
        );

        public Task<decimal> GetTotalBalanceMonthlyExpense(
            Guid userId,
            int month,
            int year,
            CancellationToken cancellationToken
        );
    }
}