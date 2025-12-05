using MediatR;

namespace Financeasy.Application.UseCases.RegisterUser
{
    public record RegisterUserCommand(string Email, string Password) : IRequest<Guid>
    {
        public string Email { get; set;}
        public string Password {get; set; }
    }
}