using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.AlertsCases.PayAlert
{
    public class PayAlertHandler : IRequestHandler<PayAlertCommand, bool>
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PayAlertHandler(
            IAlertRepository alertRepository,
            IUnitOfWork unitOfWork
        )
        {
            _alertRepository = alertRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(PayAlertCommand request, CancellationToken cancellationToken)
        {
            var alert = await _alertRepository.GetByIdAsync(request.Id, cancellationToken);

            if(alert is null)
                throw new ArgumentException("Alerta não encontrado");

            if(alert.UserId != request.UserId)
                throw new UnauthorizedAccessException("Usuário não tem permissão para realizar essa ação.");

            alert.Paid();
            _alertRepository.Update(alert);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}