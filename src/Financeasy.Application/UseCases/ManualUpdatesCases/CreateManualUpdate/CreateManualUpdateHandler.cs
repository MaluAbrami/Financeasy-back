using Financeasy.Application.Services;
using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.ManualUpdatesCases.CreateManualUpdate
{
    public class CreateManualUpdateHandler : IRequestHandler<CreateManualUpdateCommand, Guid>
    {
        private readonly IUpdateRepository _updateRepository;
        private readonly IUpdateExecutionService _updateExecutionService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateManualUpdateHandler(IUpdateRepository updateRepository, IRecurrenceRuleRepository recurrenceRuleRepository, ICategoryRepository categoryRepository, IRecurrenceEntryService recurrenceEntryService, IUpdateExecutionService updateExecutionService, IUnitOfWork unitOfWork)
        {
            _updateRepository = updateRepository;
            _updateExecutionService = updateExecutionService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateManualUpdateCommand request, CancellationToken cancellationToken)
        {
            var update = await _updateExecutionService.ExecuteAsync(
                request.UserId,
                DateTime.UtcNow,
                cancellationToken
            );

            await _updateRepository.AddAsync(update);
            await _unitOfWork.SaveChangesAsync();

            return update.Id;
        }
    }
}