using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public Task<Category?> GetByIdAndUserId(Guid id, Guid userId, CancellationToken cancellationToken);
    }
}