using MediatR;

namespace Financeasy.Application.UseCases.Login
{
    public record LoginCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set ;}
    }
}