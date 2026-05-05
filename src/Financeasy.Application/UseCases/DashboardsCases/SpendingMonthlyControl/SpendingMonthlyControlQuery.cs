using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.SpendingMonthlyControl
{
    public record SpendingMonthlyControlResponse
    (
        decimal TotalIncome,
        decimal TotalExpense
    );
    
    public class SpendingMonthlyControlQuery : IRequest<SpendingMonthlyControlResponse>
    {
        public Guid UserId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}