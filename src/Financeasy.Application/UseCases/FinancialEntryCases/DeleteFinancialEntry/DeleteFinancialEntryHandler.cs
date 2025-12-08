using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.DeleteFinancialEntry
{
    public class DeleteFinancialEntryHandler : IRequestHandler<DeleteFinancialEntryCommand, DeleteFinancialEntryCommand>
    {
        private readonly IFinancialEntryRepository _financialRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFinancialEntryHandler(IFinancialEntryRepository financialRepository, IUnitOfWork unitOfWork)
        {
            _financialRepository = financialRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteFinancialEntryCommand> Handle(DeleteFinancialEntryCommand request, CancellationToken cancellationToken)
        {
            var financialExist = await _financialRepository.GetByIdAsync(request.Id);

            if(financialExist is null)
                throw new ArgumentException($"Não foi encontrado lançamento financeiro com o id {request.Id}");

            if(financialExist.UserId != request.UserId)
                throw new UnauthorizedAccessException("Usuário não possui acesso a essa ação.");

            _financialRepository.Delete(financialExist);
            await _unitOfWork.SaveChangesAsync();

            return request;
        }
    }
}