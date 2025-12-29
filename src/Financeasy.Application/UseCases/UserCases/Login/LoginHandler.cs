using Financeasy.Application.Services;
using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.UserCases.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IUpdateExecutionService _updateExecutionService;
        private readonly IUpdateRepository _updateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LoginHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService, IUpdateExecutionService updateExecutionService, IUpdateRepository updateRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _updateExecutionService = updateExecutionService;
            _updateRepository = updateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _userRepository.GetUserByEmail(request.Email);

            if(userExist is null)
                throw new ArgumentException("Email ou senha incorretos.");

            if(!_passwordHasher.Verify(request.Password, userExist.PasswordHash))
                throw new ArgumentException("Email ou senha incorretos.");

            var newUpdate = await _updateExecutionService.ExecuteAsync(userExist.Id, DateTime.UtcNow, cancellationToken);
            await _updateRepository.AddAsync(newUpdate);
            await _unitOfWork.SaveChangesAsync();
                
            return _tokenService.GenerateToken(userExist.Id, userExist.Email);
        }
    }
}