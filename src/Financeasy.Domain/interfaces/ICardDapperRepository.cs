using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface ICardDapperRepository
    {
        public  Task<Card?> GetCardById(
            Guid cardId,
            CancellationToken cancellationToken
        );
    }
}