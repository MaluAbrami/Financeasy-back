using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.CreateFinancialEntry
{
    public class CreateFinancialEntryHandler : IRequestHandler<CreateFinancialEntryCommand, Guid>
    {
        private readonly IFinancialEntryRepository _financialRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFinancialEntryHandler(IFinancialEntryRepository financialRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _financialRepository = financialRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateFinancialEntryCommand request, CancellationToken cancellationToken)
        {
            Category? category = await _categoryRepository.GetByIdAsync(request.Data.CategoryId);
            if(category is null)
                throw new ArgumentException($"Categoria de {request.Data.CategoryId} não foi encontrada");

            FinancialEntry newFinancialEntry =             
                new FinancialEntry
                (
                    request.UserId,
                    request.Data.Amount,
                    request.Data.Description,
                    request.Data.Date,
                    category
                );

            await _financialRepository.AddAsync(newFinancialEntry);
            await _unitOfWork.SaveChangesAsync();

            return newFinancialEntry.Id;
        }
    }
}