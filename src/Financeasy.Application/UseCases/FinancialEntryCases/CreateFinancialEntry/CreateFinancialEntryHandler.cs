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
                    request.Amount,
                    request.Category,
                    request.Description,
                    request.Date,
                    request.Type,
                    request.Fixed
                );

            await _financialRepository.AddAsync(newFinancialEntry);
            await _unitOfWork.SaveChangesAsync();

            return newFinancialEntry.Id;
        }
    }
}