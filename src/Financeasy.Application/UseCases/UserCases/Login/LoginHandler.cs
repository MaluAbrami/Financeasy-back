using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.UserCases.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public LoginHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _userRepository.GetUserByEmail(request.Email, cancellationToken);

            if (userExist is null)
                throw new ArgumentException("Email ou senha incorretos.");

            if (!_passwordHasher.Verify(request.Password, userExist.PasswordHash))
                throw new ArgumentException("Email ou senha incorretos.");

            return _tokenService.GenerateToken(userExist.Id, userExist.Email);
        }
    }
}