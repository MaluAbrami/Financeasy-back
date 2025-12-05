using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteUserCommand> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _userRepository.GetByIdAsync(request.UserId);

            if(userExist is null)
                throw new ArgumentException($"Usuário com id {request.UserId} não existe.");

            _userRepository.Delete(userExist);
            await _unitOfWork.SaveChangesAsync();

            return request;
        }
    }
}