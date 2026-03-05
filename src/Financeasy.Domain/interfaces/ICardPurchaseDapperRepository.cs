using Financeasy.Domain.DTO.CardPurchase;

namespace Financeasy.Domain.interfaces
{
    public interface ICardPurchaseDapperRepository
    {
        public Task<GetPagedCardPurchaseDTO> GetPagedWithRelationsAsync(
            Guid userId,
            string orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken);

        public Task<GetPagedCardPurchaseDTO> GetPagedWithRelationsByCardAsync(
            Guid cardId,
            string orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken);
    }
}