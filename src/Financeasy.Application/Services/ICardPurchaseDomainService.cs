using Financeasy.Domain.models;

namespace Financeasy.Application.Services
{
    public interface ICardPurchaseDomainService
    {
        Task GenerateInvoicesAndInstallmentsAsync(
            Card card,
            CardPurchase purchase,
            DateTime purchaseDate,
            CancellationToken cancellationToken
        );
    }
}