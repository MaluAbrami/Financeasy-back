using MediatR;

namespace Financeasy.Application.UseCases.DashboardsCases.GetBalanceEvolution
{
    public record GetBalanceEvolutionResponse()
    {
        public List<BalanceResponse> Balances { get; set; }
    }

    public record BalanceResponse()
    {
        public string Period { get; set; }
        public decimal TotalIncomes { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalMonthBalance { get; set; }
        public decimal TotalAccumulatedBalance { get; set; }
    }

    public record GetBalanceEvolution : IRequest<GetBalanceEvolutionResponse>
    {
        public Guid UserId { get; set; }
        public int InitialYearComparison { get; set; }
        public int InitialMonthComparison { get; set; }
        public int EndYearComparison { get; set; }
        public int EndMonthComparison { get; set; }
    }
}