using Financeasy.Domain.models;

namespace Financeasy.Application.Services
{
    public interface ICardPurchaseDomainService
    {
        Task GenerateInvoicesAndInstallmentsAsync(
            Card card,
            CardPurchase purchase,
            string categoryName,
            DateTime purchaseDate,
            CancellationToken cancellationToken
        );

        Task DeleteInstallmentsAndDecreaseInvoiceAsync(
            Guid cardPurchaseId, 
            CancellationToken cancellationToken
        );
    }
}