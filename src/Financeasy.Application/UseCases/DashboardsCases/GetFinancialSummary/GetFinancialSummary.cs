using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetFinancialSummary
{
    public record GetFinancialSummaryResponse()
    {
        public decimal TotalExpense { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalBalance { get; set; }
    }

    public record GetFinancialSummary : IRequest<GetFinancialSummaryResponse >
    {
        public Guid UserId { get; set; }
    }
}