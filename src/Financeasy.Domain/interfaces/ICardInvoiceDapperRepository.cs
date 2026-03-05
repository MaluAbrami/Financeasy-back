using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface ICardInvoiceDapperRepository
    {
        public Task<CardInvoice?> GetCardInvoiceByCardIdAndClosingDate(
            Guid cardId,
            DateTime closingDate,
            CancellationToken cancellationToken
        );
    }
}