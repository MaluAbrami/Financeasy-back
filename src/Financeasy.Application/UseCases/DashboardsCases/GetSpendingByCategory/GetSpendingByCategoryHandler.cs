using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetSpendingByCategory
{
    public class GetSpendingByCategoryHandler : IRequestHandler<GetSpendingByCategory, GetSpendingByCategoryResponse>
    {
        private readonly IFinancialEntryRepository _financialRepository;
        private readonly IUserRepository _userRepository;

        public GetSpendingByCategoryHandler(IFinancialEntryRepository financialRepository, IUserRepository userRepository)
        {
            _financialRepository = financialRepository;
            _userRepository = userRepository;
        }
        
        public async Task<GetSpendingByCategoryResponse> Handle(GetSpendingByCategory request, CancellationToken cancellationToken)
        {
            var userExist = _userRepository.GetByIdAsync(request.UserId);
            if(userExist is null)
            {
                throw new UnauthorizedAccessException($"Usuário autenticado com id {request.UserId} não encontrado");
            }

            GetSpendingByCategoryResponse response = new();

            response.TotalExpense = await _financialRepository.GetTotalExpenseByCategory(request.Category, request.UserId);
            return response;
        }
    }
}