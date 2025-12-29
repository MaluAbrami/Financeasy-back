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
        private readonly IRecurrenceRuleRepository _recurrenceRuleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRecurrenceEntryService _recurrenceService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateManualUpdateHandler(IUpdateRepository updateRepository, IRecurrenceRuleRepository recurrenceRuleRepository, ICategoryRepository categoryRepository, IRecurrenceEntryService recurrenceEntryService, IUnitOfWork unitOfWork)
        {
            _updateRepository = updateRepository;
            _recurrenceRuleRepository = recurrenceRuleRepository;
            _categoryRepository = categoryRepository;
            _recurrenceService = recurrenceEntryService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateManualUpdateCommand request, CancellationToken cancellationToken)
        {
            var dateTimeNow = DateTime.Now;
            var updates = await _updateRepository.GetAllAsync();
            var lastUpdateDate = new DateTime();

            if(updates.Count > 0)
                lastUpdateDate = updates.Last().UpdateDate;

            if(lastUpdateDate.Date == dateTimeNow.Date)
                throw new ArgumentException("A atualização diária já foi realizada.");

            var recurrences = await _recurrenceRuleRepository.FindAsync(x => (x.EndDate >= dateTimeNow || x.EndDate == null) && x.UserId == request.UserId);

            Update newUpdate = new Update();

            if(recurrences.Count > 0)
            {
                var totalCount = 0;
                totalCount = await _recurrenceService.GenerateEntriesInManualUpdate(recurrences, request.UserId, lastUpdateDate, cancellationToken);

                newUpdate = new Update
                (
                    request.UserId,
                    dateTimeNow,
                    totalCount
                );   

                await _updateRepository.AddAsync(newUpdate);
                await _unitOfWork.SaveChangesAsync();

                return newUpdate.Id;
            }

            newUpdate = new Update
            (
                request.UserId,
                dateTimeNow,
                0
            );   

            await _updateRepository.AddAsync(newUpdate);
            await _unitOfWork.SaveChangesAsync();

            return newUpdate.Id;
        }
    }
}