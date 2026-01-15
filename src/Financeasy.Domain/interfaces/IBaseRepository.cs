using System.Linq.Expressions;
using Financeasy.Domain.DTO.Pagination;

namespace Financeasy.Domain.interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<T?> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void Delete(T entity);
        Task<GetPagedBaseResponseDTO<T>> GetPagedAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> orderBy, bool ascending, int page, int pageSize);
    }
}