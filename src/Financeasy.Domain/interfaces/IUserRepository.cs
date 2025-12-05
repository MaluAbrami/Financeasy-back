using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByEmail(string email);
    }
}