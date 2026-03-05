namespace Financeasy.Domain.interfaces.Services
{
    public interface ICardService
    {
        public Task<bool> CheckAvailableLimit(decimal cardCreditLimit, decimal amount, Guid cardId, CancellationToken cancellationToken);
    }
}