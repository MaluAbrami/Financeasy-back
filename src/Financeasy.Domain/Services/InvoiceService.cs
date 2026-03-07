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

        public async Task PayInvoice(Guid id, CancellationToken cancellationToken)
        {
            
        }

        public async Task<CardInvoice> GetOrGenerateInvoice(
            Card card, DateTime purchaseDate, int number, CancellationToken cancellationToken
        )
        {
            var(closingDate, dueDate) = GetClosingAndDueDate(card, purchaseDate, number);

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

            //caso ele já exista pode ser que essa fatura já tenha sido paga uma vez antes do fechamento e marcada como paga, portanto voltamos a marca-lá como não paga(isso não irá afetar as parcelas antigas dela que já estão pagas)
            invoiceExist.IsPaid = false;

            return invoiceExist;
        }

        private (DateTime closingDate, DateTime dueDate) GetClosingAndDueDate(Card card, DateTime purchaseDate, int number)
        {
            var date = purchaseDate.AddMonths(number - 1);

            var closingDate = new DateTime(date.Year, date.Month, card.ClosingDay);
            var dueDate = new DateTime(date.Year, date.Month, card.DueDay);
            if (date.Day >= card.ClosingDay)
            {
                closingDate = closingDate.AddMonths(1);
                dueDate = dueDate.AddMonths(1);
            }

            return (closingDate, dueDate);
        }
    }
}