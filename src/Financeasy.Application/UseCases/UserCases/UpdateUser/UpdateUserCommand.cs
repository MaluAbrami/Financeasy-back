using Financeasy.Domain.DTO.User;
using MediatR;

namespace Financeasy.Application.UseCases.UserCases.UpdateUser
{
    public record UpdateUserCommand : IRequest<UpdateUserCommand>
    {
        public Guid UserId { get; set; }
        public UpdateUserRequestDTO User { get; set; }
    }
}