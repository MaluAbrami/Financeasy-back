using Financeasy.Domain.DTO;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.GetAllFinancialByUser
{
    public record GetAllFinancialResponse()
    {
        public List<FinancialResponseDTO>? FinancialsByUser { get; set; }
    }

    public record GetAllFinancialByUser : IRequest<GetAllFinancialResponse>
    {
        public Guid UserId { get; set; }
    }
}