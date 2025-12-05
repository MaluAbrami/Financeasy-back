namespace Financeasy.Domain.interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId, string email);
    }
}