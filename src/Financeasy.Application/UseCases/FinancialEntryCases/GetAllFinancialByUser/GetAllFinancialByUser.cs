using Financeasy.Domain.Enums;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.GetAllFinancialByUser
{
    public record FinancialByUserResponse()
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public required string Category { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public EntryType Type { get; set; }
        public bool Fixed { get; set; }
    }

    public record GetAllFinancialByUserResponse()
    {
        public List<FinancialByUserResponse>? FinancialsByUser { get; set; }
    }

    public record GetAllFinancialByUser : IRequest<GetAllFinancialByUserResponse>
    {
        public Guid UserId { get; set; }
    }
}