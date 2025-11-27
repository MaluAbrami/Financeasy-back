namespace Financeasy.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync();
    }
}