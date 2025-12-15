using Financeasy.Domain.DTO;
using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.GetFinancialById
{
    public class GetFinancialByIdHandler : IRequestHandler<GetFinancialById, FinancialResponseDTO>
    {
        private readonly IFinancialEntryRepository _financialRepository;

        public GetFinancialByIdHandler(IFinancialEntryRepository financialRepository)
        {
            _financialRepository = financialRepository;
        }

        public async Task<FinancialResponseDTO> Handle(GetFinancialById request, CancellationToken cancellationToken)
        {
            var financialExist = await _financialRepository.GetByIdAsync(request.Id);

            if(financialExist is null)
                throw new ArgumentException($"Lançamento financeiro com o id {request.Id} não foi encontrado.");
            
            if(financialExist.UserId != request.UserId)
                throw new UnauthorizedAccessException("Usuário não tem acesso a esse tipo de ação.");

            return new FinancialResponseDTO
            {
                Id = financialExist.Id,
                Amount = financialExist.Amount,
                Description = financialExist.Description,
                Date = financialExist.Date,
                CategoryName = financialExist.CategoryName,
                Type = financialExist.Type,
                IsFixed = financialExist.IsFixed
            };
        }
    }
}