using Financeasy.Domain.DTO;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.GetAllFinancialByUser
{
    public class GetAllFinancialByUserHandler : IRequestHandler<GetAllFinancialByUser, GetAllFinancialResponse>
    {
        private readonly IFinancialEntryRepository _financialRepository;

        public GetAllFinancialByUserHandler(IFinancialEntryRepository financialRepository)
        {
            _financialRepository = financialRepository;
        }

        public async Task<GetAllFinancialResponse> Handle(GetAllFinancialByUser request, CancellationToken cancellationToken)
        {
            List<FinancialEntry> financialEntrys = await _financialRepository.FindAsync(x => x.UserId == request.UserId);

            List<FinancialResponseDTO> listResponse = [];
            if (financialEntrys.Any())
            {
                foreach (var financial in financialEntrys)
                {
                    FinancialResponseDTO responseDTO = new FinancialResponseDTO
                    {
                        Id = financial.Id,
                        Amount = financial.Amount,
                        Category = financial.Category,
                        Description = financial.Description,
                        Date = financial.Date,
                        Type = financial.Type,
                        IsFixed = financial.IsFixed
                    };

                    listResponse.Add(responseDTO);
                }
            }

            return new GetAllFinancialResponse { FinancialsByUser = listResponse };
        }
    }
}