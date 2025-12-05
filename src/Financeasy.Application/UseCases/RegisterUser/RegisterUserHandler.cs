using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var alreadyExist = await _userRepository.GetUserByEmail(request.Email);

            if(alreadyExist is not null)
                throw new ArgumentException("Já existe um usuário com esse email");

            var newUser = new User(request.Email, _passwordHasher.Hash(request.Password));
            await _userRepository.AddUser(newUser);

            return newUser.Id;
        }
    }
}