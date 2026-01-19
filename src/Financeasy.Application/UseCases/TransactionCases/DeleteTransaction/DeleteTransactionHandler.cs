using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.TransactionCases.DeleteTransaction
{
    public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, DeleteTransactionCommand>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTransactionHandler(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteTransactionCommand> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetTransactionWithCategoryAndBank(request.TransactionId);

            if(transaction is null)
                throw new ArgumentException("Transação não encontrada");

            if(transaction.UserId != request.UserId)
                throw new UnauthorizedAccessException("Usuário não tem acesso a essa ação");
            
            if(transaction.Category.Type == EntryType.Expense)
                transaction.BankAccount!.IncreaseBalance(transaction.Amount);
            else
                transaction.BankAccount!.DecreaseBalance(transaction.Amount);

            _transactionRepository.Delete(transaction);
            await _unitOfWork.SaveChangesAsync();

            return request;
        }
    }
}