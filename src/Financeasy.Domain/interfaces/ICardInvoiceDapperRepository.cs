using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface ICardInvoiceDapperRepository
    {
        public Task<CardInvoice?> GetCardInvoiceByClosingDate(
            DateTime closingDate,
            CancellationToken cancellationToken
        );
    }
}