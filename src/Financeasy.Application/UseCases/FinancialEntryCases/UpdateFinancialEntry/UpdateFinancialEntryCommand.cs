using Financeasy.Domain.DTO;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.UpdateFinancialEntry
{
    public record UpdateFinancialEntryCommand : IRequest<FinancialResponseDTO>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required FinancialUpdateRequestDTO Data { get; set; }
    }
}