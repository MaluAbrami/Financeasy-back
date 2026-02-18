using System.Linq.Expressions;
using Financeasy.Domain.DTO.Alert;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface IAlertRepository : IBaseRepository<Alert>
    {
        Task<GetPagedAlertDTO> GetPagedWithRelationsAsync(    
            Expression<Func<Alert, bool>> predicate,
            Expression<Func<Alert, object>> orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken);
    }
}