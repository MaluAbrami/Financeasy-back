using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CardInvoiceCases.GetCardInvoiceByPeriod
{
    public class GetInvoiceByPeriodHandler : IRequestHandler<GetInvoiceByPeriodQuery, GetInvoiceByPeriodResponse?>
    {
        private readonly ICardInvoiceRepository _cardInvoiceRepository;

        public GetInvoiceByPeriodHandler(ICardInvoiceRepository cardInvoiceRepository)
        {
            _cardInvoiceRepository = cardInvoiceRepository;
        }

        public async Task<GetInvoiceByPeriodResponse?> Handle(GetInvoiceByPeriodQuery request, CancellationToken cancellationToken)
        {
            var invoiceExist = await _cardInvoiceRepository.FindAsync(x => 
                x.CardId == request.CardId && 
                x.DueDate.Month == request.Month && 
                x.DueDate.Year == request.Year, 
                cancellationToken
            );
            
            CardInvoice invoice = invoiceExist.FirstOrDefault();

            if(invoice is null)
                return null;

            return new GetInvoiceByPeriodResponse
            {
                ClosingDate = invoice.ClosingDate,
                DueDate = invoice.DueDate,
                TotalAmount = invoice.TotalAmount,
                IsPaid = invoice.IsPaid
            };
        }
    }
}