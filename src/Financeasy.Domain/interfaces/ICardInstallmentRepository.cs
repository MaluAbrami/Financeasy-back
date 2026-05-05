using System.Linq.Expressions;
using Financeasy.Domain.DTO.CardInstallment;
using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface ICardInstallmentRepository : IBaseRepository<CardInstallment>
    {
         public Task<GetPagedInstallmentsDTO> GetPagedWithRelationsAsync(    
            Expression<Func<CardInstallment, bool>> predicate,
            Expression<Func<CardInstallment, object>> orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken);
    }
}