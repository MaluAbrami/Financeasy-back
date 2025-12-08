using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.CreateFinancialEntry
{
    public record CreateFinancialEntryCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public required string Category { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public EntryType Type { get; set; }
        public bool IsFixed { get; set; }
    }
}