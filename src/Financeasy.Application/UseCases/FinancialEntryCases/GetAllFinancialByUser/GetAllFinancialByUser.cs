using Financeasy.Domain.DTO;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.GetAllFinancialByUser
{
    public record GetAllFinancialResponseDTO()
    {
        public List<FinancialResponseDTO>? FinancialsByUser { get; set; }
    }

    public record GetAllFinancialByUser : IRequest<GetAllFinancialResponseDTO>
    {
        public Guid UserId { get; set; }
    }
}