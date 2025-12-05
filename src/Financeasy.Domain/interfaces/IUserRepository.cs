using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User? GetUserByEmail(string email);
        public void UpdateUser(User user);
        public void DeleteUser(User user);
    }
}