using Financeasy.Application.Services;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.RecurrenceRuleCases.CreateRecurrenceRule
{
    public class CreateRecurrenceRuleHandler : IRequestHandler<CreateRecurrenceRuleCommand, Guid>
    {
        private readonly IRecurrenceRuleRepository _recurrenceRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRecurrenceEntryService _recurrenceService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRecurrenceRuleHandler(IRecurrenceRuleRepository recurrenceRepository, ICategoryRepository categoryRepository, IRecurrenceEntryService recurrenceService, IUnitOfWork unitOfWork)
        {
            _recurrenceRepository = recurrenceRepository;
            _categoryRepository = categoryRepository;
            _recurrenceService = recurrenceService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateRecurrenceRuleCommand request, CancellationToken cancellationToken)
        {
            var categoryExist = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if(categoryExist is null)
                throw new ArgumentException($"Categoria de id {request.CategoryId} não foi encontrada.");

            if(!categoryExist.IsFixed)
                throw new ArgumentOutOfRangeException("Apenas é possível criar regra de recorrência para categorias fixas.");

            var newRecurrence = new RecurrenceRule(
                request.CategoryId,
                request.Recurrence.Frequency,
                request.Recurrence.DayOfMonth,
                request.Recurrence.DayOfWeek,
                request.Recurrence.AdjustmentRule,
                request.Recurrence.StartDate,
                request.Recurrence.EndDate,
                request.Recurrence.Amount,
                true
            );

            await _recurrenceRepository.AddAsync(newRecurrence);
            await _unitOfWork.SaveChangesAsync();

            if(newRecurrence.StartDate <= DateTime.Today)
            {
                await _recurrenceService.GenerateEntries(
                    newRecurrence,
                    categoryExist,
                    request.UserId,
                    cancellationToken
                );
            }

            return newRecurrence.Id;
        }
    }
}