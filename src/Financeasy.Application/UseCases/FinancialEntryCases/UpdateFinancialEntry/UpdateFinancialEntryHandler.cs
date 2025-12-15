using Financeasy.Domain.DTO;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.FinancialEntryCases.UpdateFinancialEntry
{
    public class UpdateFinancialEntryHandler : IRequestHandler<UpdateFinancialEntryCommand, FinancialResponseDTO>
    {
        private readonly IFinancialEntryRepository _financialRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFinancialEntryHandler(IFinancialEntryRepository financialRepository, IUnitOfWork unitOfWork)
        {
            _financialRepository = financialRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<FinancialResponseDTO> Handle(UpdateFinancialEntryCommand request, CancellationToken cancellationToken)
        {
            var financialExist = await _financialRepository.GetByIdAsync(request.Id);

            if(financialExist is null)
                throw new ArgumentException($"Não foi encontrado lançamento financeiro com o id {request.Id}");

            if(financialExist.UserId != request.UserId)
                throw new UnauthorizedAccessException("Usuário não possui acesso a essa ação.");

            financialExist.Update(new FinancialEntryUpdateDTO
            {
                Amount = request.Data.Amount,
                Description = request.Data.Description,
                Date = request.Data.Date
            });

            _financialRepository.Update(financialExist);
            await _unitOfWork.SaveChangesAsync();

            return new FinancialResponseDTO
            {
                Id = financialExist.Id,
                Amount = financialExist.Amount,
                Description = financialExist.Description,
                Date = financialExist.Date,
                CategoryName = financialExist.CategoryName,
                Type = financialExist.Type,
                IsFixed = financialExist.IsFixed
            };
        }
    }
}