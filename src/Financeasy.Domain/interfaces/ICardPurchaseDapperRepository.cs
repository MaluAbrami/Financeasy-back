using Financeasy.Domain.DTO.CardPurchase;
using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface ICardPurchaseDapperRepository
    {
        public Task<CardPurchase?> GetPurchaseById(Guid id, CancellationToken cancellationToken);

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