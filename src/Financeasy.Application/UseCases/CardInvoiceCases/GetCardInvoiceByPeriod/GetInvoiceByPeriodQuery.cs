using MediatR;

namespace Financeasy.Application.UseCases.CardInvoiceCases.GetCardInvoiceByPeriod
{
    public record GetInvoiceByPeriodResponse()
    {
        public Guid Id { get; set; }
        public DateTime ClosingDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public record GetInvoiceByPeriodQuery : IRequest<GetInvoiceByPeriodResponse?>
    {
        public Guid CardId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}