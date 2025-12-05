using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var alreadyExist = _userRepository.GetUserByEmail(request.Email);

            if(alreadyExist is not null)
                throw new ArgumentException("Já existe um usuário com esse email");

            var newUser = new User(request.Email, request.Password);
            _userRepository.AddUser(newUser);

            return Task.FromResult(newUser.Id);
        }
    }
}