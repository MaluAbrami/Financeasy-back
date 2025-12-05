using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(Guid id);
        public void UpdateUser(User user);
        public void DeleteUser(User user);
    }
}