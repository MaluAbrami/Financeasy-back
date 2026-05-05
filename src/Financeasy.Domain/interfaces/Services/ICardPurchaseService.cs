namespace Financeasy.Domain.interfaces.Services
{
    public interface ICardPurchaseService
    {
        public Task<Guid> CreatePurchase(
            Guid userId, Guid cardId, Guid categoryId, decimal totalAmount, int installments, DateTime purchaseDate, string description, CancellationToken cancellationToken
        );
    }
}