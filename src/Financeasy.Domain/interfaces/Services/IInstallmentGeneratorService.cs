using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces.Services
{
    public interface IInstallmentGeneratorService
    {
        public Task GenerateInstallments(
            CardPurchase purchase, Card card, CancellationToken cancellationToken
        );
    }
}