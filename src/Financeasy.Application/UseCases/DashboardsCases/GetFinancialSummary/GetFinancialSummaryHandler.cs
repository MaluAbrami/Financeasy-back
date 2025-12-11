using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetFinancialSummary
{
    public class GetFinancialSummaryHandler : IRequestHandler<GetFinancialSummary, GetFinancialSummaryResponse>
    {
        private readonly IFinancialEntryRepository _financialRepository;
        private readonly IUserRepository _userRepository;

        public GetFinancialSummaryHandler(IFinancialEntryRepository financialRepository, IUserRepository userRepository)
        {
            _financialRepository = financialRepository;
            _userRepository = userRepository;
        }

        public async Task<GetFinancialSummaryResponse> Handle(GetFinancialSummary request, CancellationToken cancellationToken)
        {
            var userExist = await _userRepository.GetByIdAsync(request.UserId);
            if(userExist is null)
            {
                throw new UnauthorizedAccessException($"Usuário autenticado com id {request.UserId} não encontrado");
            }

            GetFinancialSummaryResponse response = new();

            response.TotalExpense = await _financialRepository.GetTotalAmountByType(EntryType.Expense, request.UserId);
            response.TotalIncome = await _financialRepository.GetTotalAmountByType(EntryType.Income, request.UserId);
            response.TotalBalance = response.TotalIncome - response.TotalExpense;

            return response;
        }
    }
}