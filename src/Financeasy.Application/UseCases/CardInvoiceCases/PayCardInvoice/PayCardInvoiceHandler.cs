using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CardInvoiceCases.PayCardInvoice
{
    public class PayCardInvoiceHandler : IRequestHandler<PayCardInvoiceCommand, Guid>
    {
        private readonly ICardInvoiceRepository _cardInvoiceRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ICardInstallmentRepository _cardInstallmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PayCardInvoiceHandler(
            ICardInvoiceRepository cardInvoiceRepository, 
            ITransactionRepository transactionRepository, 
            ICardRepository cardRepository,
            IBankAccountRepository bankAccountRepository,
            ICardInstallmentRepository cardInstallmentRepository,
            IUnitOfWork unitOfWork)
        {
            _cardInvoiceRepository = cardInvoiceRepository;
            _transactionRepository = transactionRepository;
            _cardRepository = cardRepository;
            _bankAccountRepository = bankAccountRepository;
            _cardInstallmentRepository = cardInstallmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(PayCardInvoiceCommand request, CancellationToken cancellationToken)
        {
            var cardInvoice = await _cardInvoiceRepository.FindAsync(x => x.CardId == request.CardId && x.ClosingDate == request.ClosingDate, cancellationToken);
            if(!cardInvoice.Any())
                throw new ArgumentException($"Não foi encontrado nenhuma fatura para a data {request.ClosingDate.Date}");

            var cardInvoiceExist = cardInvoice.First();

            if(cardInvoiceExist.IsPaid)
                throw new ArgumentException("Essa fatura já está paga.");

            var card = await _cardRepository.GetByIdAsync(request.CardId, cancellationToken);

            var bankExists = await _bankAccountRepository.GetByIdAsync(card.BankAccountId, cancellationToken);

            var newTransaction = new Transaction(
                PaymentMethod.Transfer,
                request.UserId,
                card.BankAccountId,
                card.CategoryId,
                cardInvoiceExist.TotalAmount,
                DateTime.UtcNow,
                "Pagamento de fatura do cartão de crédito"
            );

            await _transactionRepository.AddAsync(newTransaction, cancellationToken);

            cardInvoiceExist.IsPaid = true;

            var cardInstallments = await _cardInstallmentRepository.FindAsync(x => x.CardInvoiceId == cardInvoiceExist.Id, cancellationToken);
            foreach ( var installment in cardInstallments)
            {
                installment.Paid = true;
            }

            card.IncreaseAvailableLimit(cardInvoiceExist.TotalAmount);

            bankExists.DecreaseBalance(cardInvoiceExist.TotalAmount);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return cardInvoiceExist.Id;
        }
    }
}