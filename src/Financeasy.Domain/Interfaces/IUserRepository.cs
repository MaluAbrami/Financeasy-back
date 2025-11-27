using Financeasy.Domain.Models;

namespace Financeasy.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User> GetUserByIdWithSettingsAysnc(Guid id);
    }
}