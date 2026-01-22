using System.Linq.Expressions;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.interfaces;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly FinanceasyDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(FinanceasyDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<GetPagedBaseResponseDTO<T>> GetPagedAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>> orderBy, bool ascending, int page, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<T> query = _dbSet;

            if ( filter is not null)
                query = query.Where(filter);

            var totalItems = await query.CountAsync();

            query = ascending
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);

            return new GetPagedBaseResponseDTO<T> 
            {   
                List = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(),
                TotalItems = totalItems
            };
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}