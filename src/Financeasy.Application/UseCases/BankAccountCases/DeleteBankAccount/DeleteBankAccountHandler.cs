using Financeasy.Application.UseCases.BankAccountCases.DeleteBankAccount;
using Financeasy.Domain.interfaces;
using MediatR;

public class DeleteBankAccountHandler 
    : IRequestHandler<DeleteBankAccountCommand, Unit>
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly ICardRepository _cardRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBankAccountHandler(
        IBankAccountRepository bankAccountRepository,
        ICardRepository cardRepository,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _bankAccountRepository = bankAccountRepository;
        _cardRepository = cardRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        DeleteBankAccountCommand request, 
        CancellationToken cancellationToken)
    {
        var bank = await _bankAccountRepository
            .GetByIdAsync(request.BankAccountId, cancellationToken);

        if (bank is null)
            throw new ArgumentException("Conta bancária não encontrada");

        if (bank.UserId != request.UserId)
            throw new UnauthorizedAccessException("Usuário não tem acesso a esta ação");

        var cards = (await _cardRepository
            .FindAsync(x => x.BankAccountId == request.BankAccountId, cancellationToken))
            .ToList();

        var transactions = (await _transactionRepository
            .FindAsync(x => x.BankAccountId == request.BankAccountId, cancellationToken))
            .ToList();

        var canBeDeleted = !cards.Any() && !transactions.Any();

        if (canBeDeleted)
        {
            _bankAccountRepository.Delete(bank);
        }
        else
        {
            bank.DisableBankAccount();
            cards.ForEach(card => card.DisableCard());
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
