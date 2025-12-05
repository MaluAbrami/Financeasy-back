using Financeasy.Domain.interfaces;
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

        public Task<Guid> Handle(CreateFinancialEntryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}