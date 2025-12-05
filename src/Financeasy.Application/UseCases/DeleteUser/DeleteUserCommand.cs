using MediatR;

namespace Financeasy.Application.UseCases.DeleteUser
{
    public record DeleteUserCommand(Guid UserId) : IRequest<DeleteUserCommand>
    {
    }
}