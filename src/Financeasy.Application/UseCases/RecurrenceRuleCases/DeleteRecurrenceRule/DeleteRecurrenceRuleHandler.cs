using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.RecurrenceRuleCases.DeleteRecurrenceRule
{
    public class DeleteRecurrenceRuleHandler : IRequestHandler<DeleteRecurrenceRuleCommand, DeleteRecurrenceRuleCommand>
    {        
        private readonly IRecurrenceRuleRepository _recurrenceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRecurrenceRuleHandler(IRecurrenceRuleRepository recurrenceRepository, IUnitOfWork unitOfWork)
        {
            _recurrenceRepository = recurrenceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteRecurrenceRuleCommand> Handle(DeleteRecurrenceRuleCommand request, CancellationToken cancellationToken)
        {
            var recurrenceExists = await _recurrenceRepository.GetByIdAsync(request.Id);
            if(recurrenceExists is null)
                throw new ArgumentException($"Recorrência de id {request.Id} não foi encontrada.");

            if(recurrenceExists.UserId != request.UserId)
                throw new UnauthorizedAccessException("Usuário não tem acesso a essa ação.");

            _recurrenceRepository.Delete(recurrenceExists);
            await _unitOfWork.SaveChangesAsync();

            return request;
        }
    }
}