namespace Financeasy.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> CreateAsync(T entity);
        Task<T?> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        void Update(T entity);
        void Delete(T entity);
    }
}