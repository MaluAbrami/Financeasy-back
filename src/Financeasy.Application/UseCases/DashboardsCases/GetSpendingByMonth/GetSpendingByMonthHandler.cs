using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetSpendingByMonth
{
    public class GetSpendingByMonthHandler : IRequestHandler<GetSpendingByMonth, GetSpendingByMonthResponse>
    {
        private readonly IFinancialEntryRepository _financialRepository;
        private readonly IUserRepository _userRepository;

        public GetSpendingByMonthHandler(IFinancialEntryRepository financialRepository, IUserRepository userRepository)
        {
            _financialRepository = financialRepository;
            _userRepository = userRepository;
        }

        public async Task<GetSpendingByMonthResponse> Handle(GetSpendingByMonth request, CancellationToken cancellationToken)
        {
            var userExist = await _userRepository.GetByIdAsync(request.UserId);
            if(userExist is null)
            {
                throw new UnauthorizedAccessException($"Usuário autenticado com id {request.UserId} não encontrado");
            }

            GetSpendingByMonthResponse response = new();

            response.TotalExpense = await _financialRepository.GetTotalExpenseByMonth(request.Year, request.Month, request.UserId);
            return response;
        }
    }
}