using MediatR;

namespace Financeasy.Application.UseCases.UserCases.RegisterUser
{
    public record RegisterUserCommand() : IRequest<Guid>
    {
        public required string Email { get; set;}
        public required string Password {get; set; }
    }
}