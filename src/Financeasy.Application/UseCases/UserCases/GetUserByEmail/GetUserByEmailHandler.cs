using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.UserCases.GetUserByEmail
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, GetUserByEmailResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserByEmailResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var userExist = await _userRepository.GetUserByEmail(request.Email, cancellationToken);

            if(userExist is null)
                throw new ArgumentException($"Não existe usuário com o email {request.Email}");

            return new GetUserByEmailResponse { Id = userExist.Id, Email = userExist.Email };
        }
    }
}