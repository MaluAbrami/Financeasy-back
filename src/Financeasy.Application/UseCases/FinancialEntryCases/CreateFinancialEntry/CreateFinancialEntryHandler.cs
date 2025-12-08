using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.CreateFinancialEntry
{
    public class CreateFinancialEntryHandler : IRequestHandler<CreateFinancialEntryCommand, Guid>
    {
        private readonly IFinancialEntryRepository _financialRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFinancialEntryHandler(IFinancialEntryRepository financialRepository, IUnitOfWork unitOfWork)
        {
            _financialRepository = financialRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateFinancialEntryCommand request, CancellationToken cancellationToken)
        {
            FinancialEntry newFinancialEntry =             
                new FinancialEntry
                (
                    request.UserId,
                    request.Data.Amount,
                    request.Data.Category,
                    request.Data.Description,
                    request.Data.Date,
                    request.Data.Type,
                    request.Data.IsFixed
                );

            await _financialRepository.AddAsync(newFinancialEntry);
            await _unitOfWork.SaveChangesAsync();

            return newFinancialEntry.Id;
        }
    }
}