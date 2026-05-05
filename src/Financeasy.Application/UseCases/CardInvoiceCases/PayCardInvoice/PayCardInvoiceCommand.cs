using MediatR;

namespace Financeasy.Application.UseCases.CardInvoiceCases.PayCardInvoice
{
    public record PayCardInvoiceCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid CardId { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}