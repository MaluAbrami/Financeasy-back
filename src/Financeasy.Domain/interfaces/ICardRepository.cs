using System.Linq.Expressions;
using Financeasy.Domain.DTO.Card;
using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        public Task<GetPagedCardDTO> GetPagedWithRelationsAsync(    
            Expression<Func<Card, bool>> predicate,
            Expression<Func<Card, object>> orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken);
    }
}