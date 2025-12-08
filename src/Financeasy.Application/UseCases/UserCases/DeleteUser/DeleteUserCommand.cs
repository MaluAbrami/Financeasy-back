using MediatR;

namespace Financeasy.Application.UseCases.UserCases.DeleteUser
{
    public record DeleteUserCommand(Guid UserId) : IRequest<DeleteUserCommand>
    {
    }
}