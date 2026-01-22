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
            var userExist = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if(userExist is null)
                throw new ArgumentException($"Usuário com id {request.UserId} não existe.");

            if(!string.IsNullOrEmpty(request.User.ProfilePhoto))
                userExist.ProfilePhoto = request.User.ProfilePhoto;

            if(request.User.AlertLimit != 0)
                userExist.AlertLimit = request.User.AlertLimit;

            if(request.User.Email is not null)
                userExist.Email = request.User.Email;

            _userRepository.Update(userExist);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return request;
        }
    }
}