using System.Linq.Expressions;
using Financeasy.Domain.DTO.Pagination;

namespace Financeasy.Domain.interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        void Update(T entity);
        void Delete(T entity);
        Task<GetPagedBaseResponseDTO<T>> GetPagedAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> orderBy, bool ascending, int page, int pageSize, CancellationToken cancellationToken);
    }
}