using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.BankAccountCases.UpdateAccountBalance
{
    public class UpdateAccountBalanceHandler : IRequestHandler<UpdateAccountBalance, UpdateAccountBalanceResponse>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAccountBalanceHandler(IBankAccountRepository bankAccountRepository, IUnitOfWork unitOfWork)
        {
            _bankAccountRepository = bankAccountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateAccountBalanceResponse> Handle(UpdateAccountBalance request, CancellationToken cancellationToken)
        {
            var bankAccount = await _bankAccountRepository.GetByIdAsync(request.Id, cancellationToken);

            if(bankAccount is null)
                throw new ArgumentException("Conta bancária não encontrada");

            if(bankAccount.UserId != request.UserId)
                throw new UnauthorizedAccessException("Usuário não tem acesso a essa ação");

            bankAccount.Balance = request.Balance;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdateAccountBalanceResponse
            {
                Id = bankAccount.Id,
                Bank = bankAccount.Bank,
                Balance = bankAccount.Balance
            };
        }
    }
}