using Financeasy.Domain.Interfaces;
using Financeasy.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly FinanceasyDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(FinanceasyDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual void Update(T entityUpdated)
        {
            _dbSet.Update(entityUpdated);
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}