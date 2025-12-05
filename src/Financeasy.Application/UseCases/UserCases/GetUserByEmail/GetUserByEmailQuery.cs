using MediatR;

namespace Financeasy.Application.UseCases.UserCases.GetUserByEmail
{
    public record GetUserByEmailResponse()
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }

    public record GetUserByEmailQuery : IRequest<GetUserByEmailResponse>
    {
        public required string Email { get; set; }
    }
}