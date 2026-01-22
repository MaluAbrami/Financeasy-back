using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.TransactionCases.CreateTransaction
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTransactionHandler(
            ITransactionRepository transactionRepository,
            IBankAccountRepository bankAccountRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork
        )
        {
            _transactionRepository = transactionRepository;
            _bankAccountRepository = bankAccountRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var bankExists = await _bankAccountRepository.GetByIdAsync(request.BankAccountId, cancellationToken);
            if(bankExists is null || bankExists.UserId != request.UserId)
                throw new ArgumentException("Conta bancária inválida");

            var categoryExists = await _categoryRepository.GetByIdAndUserId(request.CategoryId, request.UserId, cancellationToken); 
            if(categoryExists is null)
                throw new ArgumentException("Categoria não encontrada");

            var newTransaction = new Transaction(
                request.PaymentMethod,
                request.UserId,
                request.BankAccountId,
                request.CategoryId,
                request.Amount,
                request.Date,
                request.Description
            );

            await _transactionRepository.AddAsync(newTransaction, cancellationToken);

            if(categoryExists.Type == EntryType.Income)
                bankExists.IncreaseBalance(newTransaction.Amount);
            else
                bankExists.DecreaseBalance(newTransaction.Amount);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newTransaction.Id;
        }
    }
}