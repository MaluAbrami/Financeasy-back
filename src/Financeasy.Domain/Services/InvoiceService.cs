using Financeasy.Domain.interfaces;
using Financeasy.Domain.interfaces.Services;
using Financeasy.Domain.models;

namespace Financeasy.Domain.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ICardInvoiceRepository _invoiceRepository;
        private readonly ICardInvoiceDapperRepository _invoiceDapperRepository;

        public InvoiceService(
            ICardInvoiceRepository invoiceRepository,
            ICardInvoiceDapperRepository invoiceDapperRepository
        )
        {
            _invoiceRepository = invoiceRepository;
            _invoiceDapperRepository = invoiceDapperRepository;
        }

        public async Task<CardInvoice> GetOrGenerateInvoice(
            Card card, DateTime purchaseDate, CancellationToken cancellationToken
        )
        {
            var(closingDate, dueDate) = GetClosingAndDueDate(card, purchaseDate);

            var invoiceExist = await _invoiceDapperRepository.GetCardInvoiceByCardIdAndClosingDate(card.Id, closingDate, cancellationToken);

            if (invoiceExist is null)
            {
                var newInvoice = new CardInvoice
                (
                    card.Id,
                    closingDate,
                    dueDate
                );

                await _invoiceRepository.AddAsync(newInvoice, cancellationToken);

                return newInvoice;
            }

            return invoiceExist;
        }

        private (DateTime closingDate, DateTime dueDate) GetClosingAndDueDate(Card card, DateTime purchaseDate)
        {
            var closingDate = new DateTime(purchaseDate.Year, purchaseDate.Month, card.ClosingDay);
            var dueDate = new DateTime(purchaseDate.Year, purchaseDate.Month, card.DueDay);
            if (purchaseDate.Day >= card.ClosingDay)
            {
                closingDate = closingDate.AddMonths(1);
                dueDate = dueDate.AddMonths(1);
            }

            return (closingDate, dueDate);
        }
    }
}