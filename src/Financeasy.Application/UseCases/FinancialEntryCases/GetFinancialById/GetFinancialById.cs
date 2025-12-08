using Financeasy.Domain.DTO;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.GetFinancialById
{
    public record GetFinancialById : IRequest<FinancialResponseDTO>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}