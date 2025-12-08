using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.UserCases.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<UpdateUserCommand> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _userRepository.GetByIdAsync(request.UserId);

            if(userExist is null)
                throw new ArgumentException($"Usuário com id {request.UserId} não existe.");

            if(request.User.NewPassword is not null && request.User.OldPassword is not null)
            {
                if(!_passwordHasher.Verify(request.User.OldPassword, userExist.PasswordHash))
                    throw new ArgumentException("Senha errada");

                var newPasswordHash = _passwordHasher.Hash(request.User.NewPassword);
                userExist.PasswordHash = newPasswordHash;
            }

            if(request.User.Email is not null)
                userExist.Email = request.User.Email;

            _userRepository.Update(userExist);
            await _unitOfWork.SaveChangesAsync();
            
            return request;
        }
    }
}