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

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<DeleteUserCommand> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _userRepository.GetUserById(request.UserId);

            if(userExist is null)
                throw new ArgumentException($"Usuário com id {request.UserId} não existe.");

            _userRepository.DeleteUser(userExist);

            return request;
        }
    }
}