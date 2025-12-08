using Financeasy.Domain.DTO;
using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.UpdateFinancialEntry
{
    public record UpdateFinancialEntryCommand : IRequest<FinancialResponseDTO>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public EntryType Type { get; set; }
        public bool Fixed { get; set; }
    }
}