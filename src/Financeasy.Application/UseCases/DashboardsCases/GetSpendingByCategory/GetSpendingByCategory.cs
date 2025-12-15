using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetSpendingByCategory
{
    public record GetSpendingByCategoryResponse()
    {
        public decimal TotalExpense { get; set; }
    }

    public record GetSpendingByCategory : IRequest<GetSpendingByCategoryResponse>
    {
        public Guid UserId { get; set; }
        public required string Category { get; set; }
    }
}