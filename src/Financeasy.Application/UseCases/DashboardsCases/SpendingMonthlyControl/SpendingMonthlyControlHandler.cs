using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.SpendingMonthlyControl
{
    public class SpendingMonthlyControlHandler : IRequestHandler<SpendingMonthlyControlQuery, SpendingMonthlyControlResponse>
    {
        private readonly ITransactionDapperRepository _transactionDapperRepository;

        public SpendingMonthlyControlHandler(
            ITransactionDapperRepository transactionDapperRepository
        )
        {
            _transactionDapperRepository = transactionDapperRepository;
        }

        public async Task<SpendingMonthlyControlResponse> Handle(SpendingMonthlyControlQuery request, CancellationToken cancellationToken)
        {
            var totalIncome = await _transactionDapperRepository.GetTotalBalanceMonthlyIncome(request.UserId, request.Month, request.Year, cancellationToken);
            var totalExpense = await _transactionDapperRepository.GetTotalBalanceMonthlyExpense(request.UserId, request.Month, request.Year, cancellationToken);

            return new SpendingMonthlyControlResponse 
            (
                totalIncome,
                totalExpense
            );
        }
    }
}