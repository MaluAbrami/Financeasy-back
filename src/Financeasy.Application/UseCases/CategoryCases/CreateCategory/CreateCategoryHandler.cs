using Financeasy.Application.Factory;
using Financeasy.Application.Services;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CategoryCases.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRecurrenceRuleRepository _recurrenceRepository;
        private readonly IRecurrenceEntryService _recurrenceService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryHandler(ICategoryRepository categoryRepository, IRecurrenceRuleRepository recurrenceRepository, IRecurrenceEntryService recurrenceService, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _recurrenceRepository = recurrenceRepository;
            _recurrenceService = recurrenceService;
            _unitOfWork = unitOfWork; 
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var duplicateName = await _categoryRepository.FindAsync(x => x.Name == request.Name && x.UserId == request.UserId);
            if(duplicateName.Count > 0)
                throw new ArgumentException("Já existe uma categoria com esse nome.");

            var newCategory = new Category(request.UserId, request.Name, request.Type, request.IsFixed);

            await _categoryRepository.AddAsync(newCategory);

            if(newCategory.IsFixed)
            {
                if(request.Recurrence is null)
                    throw new ArgumentException("Para categoria fixa as informações de recorrência são obrigatórias.");

                var newRecurrence = new RecurrenceRule
                (
                    newCategory.Id,
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

                if(newRecurrence.StartDate.Date <= DateTime.Now.Date)
                {
                    await _recurrenceService.GenerateEntries(
                        newRecurrence,
                        newCategory,
                        request.UserId,
                        cancellationToken
                    );
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return newCategory.Id;
        }
    }
}