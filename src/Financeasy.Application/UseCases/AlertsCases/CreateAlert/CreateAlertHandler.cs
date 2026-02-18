using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.AlertsCases.CreateAlert
{
    public class CreateAlertHandler : IRequestHandler<CreateAlertCommand, Guid>
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAlertHandler(
            IAlertRepository alertRepository,
            IUnitOfWork unitOfWork
        )
        {
            _alertRepository = alertRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateAlertCommand request, CancellationToken cancellationToken)
        {
            Alert newAlert = new Alert(
                request.UserId,
                request.CategoryId,
                request.RecurrenceType,
                request.DueDate,
                request.ExpectedAmount
            );

            await _alertRepository.AddAsync(newAlert, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newAlert.Id;
        }
    }
}