using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetSpendingByMonth
{
    public record GetSpendingByMonthResponse()
    {
        public decimal TotalExpense { get; set; }
    }

    public record GetSpendingByMonth : IRequest<GetSpendingByMonthResponse>
    {
        public Guid UserId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}