using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetBalanceEvolution
{
    public class GetBalanceEvolutionHandler : IRequestHandler<GetBalanceEvolution, GetBalanceEvolutionResponse>
    {
        private readonly IFinancialEntryRepository _financialRepository;
        private readonly IUserRepository _userRepository;

        public GetBalanceEvolutionHandler(IFinancialEntryRepository financialRepository, IUserRepository userRepository)
        {
            _financialRepository = financialRepository;
            _userRepository = userRepository;
        }

        public async Task<GetBalanceEvolutionResponse> Handle(GetBalanceEvolution request, CancellationToken cancellationToken)
        {
            var userExist = await _userRepository.GetByIdAsync(request.UserId);
            if(userExist is null)
            {
                throw new UnauthorizedAccessException($"Usuário autenticado com id {request.UserId} não encontrado");
            }

            List<BalanceResponse> list = [];
            bool first = true;
            decimal totalAccumulatedBalance = 0;
            decimal totalIncomes = 0;
            decimal totalExpenses = 0;
            decimal totalPeriods = 0;
            int maxMonths = 12;
            int minMonths = 1;

            for(int i = request.InitialYearComparison; i <= request.EndYearComparison; i++)
            {
                if(first)
                {
                    minMonths = request.InitialMonthComparison;
                    first = false;
                }

                if(i == request.EndYearComparison)
                    maxMonths = request.EndMonthComparison;
                
                for(int j = minMonths; j <= maxMonths; j++)
                {
                    BalanceResponse balanceResponse = new();

                    balanceResponse.TotalExpenses = await _financialRepository.GetTotalAmountByTypeAndByMonth(
                            EntryType.Expense,
                            request.UserId,
                            i,
                            j
                        );
                    totalExpenses += balanceResponse.TotalExpenses;

                    balanceResponse.TotalIncomes = await _financialRepository.GetTotalAmountByTypeAndByMonth(
                            EntryType.Income,
                            request.UserId,
                            i,
                            j
                        );
                    totalIncomes += balanceResponse.TotalIncomes;

                    balanceResponse.TotalMonthBalance = balanceResponse.TotalIncomes - balanceResponse.TotalExpenses;
                    totalPeriods += balanceResponse.TotalMonthBalance;

                    totalAccumulatedBalance += balanceResponse.TotalMonthBalance;
                    balanceResponse.TotalAccumulatedBalance = totalAccumulatedBalance;
                
                    balanceResponse.Period = $"{j}/{i}";

                    list.Add(balanceResponse);
                }

                if(minMonths != 1)
                    minMonths = 1;
            }

            var lastTotalBalance = new BalanceResponse { Period = "Total", TotalIncomes = totalIncomes, TotalExpenses = totalExpenses, TotalMonthBalance = totalPeriods, TotalAccumulatedBalance = totalAccumulatedBalance };
            list.Add(lastTotalBalance);

            return new GetBalanceEvolutionResponse { Balances = list };
        }
    }
}