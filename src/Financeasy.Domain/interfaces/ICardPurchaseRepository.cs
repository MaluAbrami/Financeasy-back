using System.Linq.Expressions;
using Financeasy.Domain.DTO.CardPurchase;
using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface ICardPurchaseRepository : IBaseRepository<CardPurchase>
    {
        public Task<GetPagedCardPurchaseDTO> GetPagedWithRelationsAsync(    
            Expression<Func<CardPurchase, bool>> predicate,
            Expression<Func<CardPurchase, object>> orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken);
    }
}