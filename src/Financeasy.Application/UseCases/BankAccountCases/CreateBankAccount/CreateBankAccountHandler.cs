using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.BankAccountCases.CreateBankAccount
{
    public class CreateBankAccountHandler : IRequestHandler<CreateBankAccountCommand, Guid>
    {
        private readonly IBankAccountRepository _bankRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBankAccountHandler(IBankAccountRepository bankRepository, IUnitOfWork unitOfWork)
        {
            _bankRepository = bankRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var bankExist = await _bankRepository.FindAsync(x => x.Bank == request.Bank && x.UserId == request.UserId);

            if(bankExist.Any())
                throw new ArgumentException("Já existe uma conta desse banco.");

            BankAccount newBankAccount = new BankAccount(
                request.UserId,
                request.Bank,
                request.Balance
            );

            await _bankRepository.AddAsync(newBankAccount);
            await _unitOfWork.SaveChangesAsync();

            return newBankAccount.Id;
        }
    }
}