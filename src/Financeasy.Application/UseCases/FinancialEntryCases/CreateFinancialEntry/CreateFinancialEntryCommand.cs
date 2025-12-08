using Financeasy.Domain.DTO;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.CreateFinancialEntry
{
    public record CreateFinancialEntryCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public required FinancialCreateRequestDTO Data { get; set; }
    }
}