namespace Financeasy.Domain.interfaces
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}