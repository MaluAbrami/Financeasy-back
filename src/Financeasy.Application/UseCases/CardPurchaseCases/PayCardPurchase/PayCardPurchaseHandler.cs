using Financeasy.Domain.Enums;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.interfaces.Services;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.CardPurchaseCases.PayCardPurchase
{
    public class PayCardPurchaseHandler : IRequestHandler<PayCardPurchaseCommand, PayCardPurchaseResponse>
    {
        private readonly ICardPurchaseDapperRepository _purchaseDapperRepository;
        private readonly ICardInstallmentsDapperRepository _installmentDapperRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICardDapperRepository _cardDapperRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PayCardPurchaseHandler(
            ICardPurchaseDapperRepository purchaseDapperRepository,
            ICardInstallmentsDapperRepository installmentDapperRepository,
            ITransactionRepository transactionRepository,
            ICardDapperRepository cardDapperRepository,
            IUnitOfWork unitOfWork
        )
        {
            _purchaseDapperRepository = purchaseDapperRepository;
            _installmentDapperRepository = installmentDapperRepository;
            _transactionRepository = transactionRepository;
            _cardDapperRepository = cardDapperRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PayCardPurchaseResponse> Handle(PayCardPurchaseCommand request, CancellationToken cancellationToken)
        {
            var purchase = await _purchaseDapperRepository.GetPurchaseById(request.PurchaseId, cancellationToken);

            if(purchase is null)
                throw new ArgumentException($"Não foi encontrado uma compra no cartão de crédito com o id {request.PurchaseId}");

            var installmentAmount = request.Amount / request.InstallmentsQuantity;
            await _installmentDapperRepository.PayAdvanceInstallmentsByPurchaseId(purchase.Id, request.InstallmentsQuantity, installmentAmount, cancellationToken);

            var card = await _cardDapperRepository.GetCardById(purchase.CardId, cancellationToken);

            var transaction = new Transaction
            (
                request.UserId,
                card!.BankAccountId,
                Guid.Parse("b6723ecd-1c81-11f1-a4b2-e0d55ef153e5"), //DEPOIS PRECISO CRIAR CATEGORIAS FIXAS DO SISTEMA PARA ESSES CASOS
                PaymentMethod.Transfer,
                request.Amount,
                purchase.PurchaseDate,
                $"{purchase.Description} - {request.InstallmentsQuantity} Parcelas"
            );

            await _transactionRepository.AddAsync(transaction, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new PayCardPurchaseResponse ( transaction.Id );
        }
    }
}