using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.AlertsCases.DeleteAlert
{
    public class DeleteAlertHandler : IRequestHandler<DeleteAlertCommand, DeleteAlertCommand>
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAlertHandler(
            IAlertRepository alertRepository,
            IUnitOfWork unitOfWork
        )
        {
            _alertRepository = alertRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteAlertCommand> Handle(DeleteAlertCommand request, CancellationToken cancellationToken)
        {
            var alert = await _alertRepository.GetByIdAsync(request.Id, cancellationToken);

            if(alert is null)
                throw new Exception("Alerta não encontrado");

            if(alert.UserId != request.UserId)
                throw new UnauthorizedAccessException("Usuário não tem acesso para realizar essa ação");
                
            _alertRepository.Delete(alert);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return request;
        }
    }
}