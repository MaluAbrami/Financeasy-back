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
            var updates = await _updateRepository.GetAllAsync();
            var lastUpdateDate = new DateTime();

            if(updates.Count > 0)
                lastUpdateDate = updates.Last().UpdateDate;

            if(lastUpdateDate.Date == DateTime.Now.Date)
                throw new ArgumentException("A atualização diária já foi realizada.");

            var recurrences = await _recurrenceRuleRepository.FindAsync(x => x.StartDate <= lastUpdateDate.AddDays(1) && x.EndDate >= DateTime.Now);

            Update newUpdate = new Update();

            if(recurrences.Count > 0)
            {
                var totalCount = 0;
                foreach(var recurrence in recurrences)
                {
                    var category = await _categoryRepository.GetByIdAsync(recurrence.CategoryId);
                    if(category is null)
                        throw new ArgumentException($"Categoria de id {recurrence.CategoryId} não foi encontrada.");

                    var count = await _recurrenceService.GenerateEntries(recurrence, category, request.UserId, cancellationToken);
                    totalCount += count;
                }

                newUpdate = new Update
                (
                    request.UserId,
                    DateTime.Now,
                    totalCount
                );   

                await _updateRepository.AddAsync(newUpdate);
                await _unitOfWork.SaveChangesAsync();

                return newUpdate.Id;
            }

            newUpdate = new Update
            (
                request.UserId,
                DateTime.Now,
                0
            );   

            await _updateRepository.AddAsync(newUpdate);
            await _unitOfWork.SaveChangesAsync();

            return newUpdate.Id;
        }
    }
}