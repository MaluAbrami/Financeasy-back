using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.CreateFinancialEntry
{
    public record CreateFinancialEntryCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public EntryType Type { get; set; }
        public bool Fixed { get; set; }
    }
}