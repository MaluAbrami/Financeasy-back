using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.BankAccount.CreateBankAccount
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
            throw new NotImplementedException();
        }
    }
}