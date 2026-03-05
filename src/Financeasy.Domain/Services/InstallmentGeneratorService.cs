using Financeasy.Domain.interfaces;
using Financeasy.Domain.interfaces.Services;
using Financeasy.Domain.models;

namespace Financeasy.Domain.Services
{
    public class InstallmentGeneratorService : IInstallmentGeneratorService
    {
        private readonly ICardInstallmentRepository _cardInstallmentRepository;
        private readonly IInvoiceService _invoiceService;

        public InstallmentGeneratorService(
            ICardInstallmentRepository cardInstallmentRepository,
            IInvoiceService invoiceService
        )
        {
            _cardInstallmentRepository = cardInstallmentRepository;
            _invoiceService = invoiceService;
        }

        public async Task GenerateInstallments(
            CardPurchase purchase, Card card, CancellationToken cancellationToken
        )
        {
            var installmentAmount = purchase.TotalAmount / purchase.Installments;

            List<CardInstallment> installments = [];

            for (int i = 1; i <= purchase.Installments; i++)
            {
                var invoice = await _invoiceService.GetOrGenerateInvoice(card, purchase.PurchaseDate, cancellationToken);

                var newInstallment = new CardInstallment
                (
                    purchase.Id,
                    invoice.Id,
                    i,
                    installmentAmount
                );

                installments.Add(newInstallment);
            }

            await _cardInstallmentRepository.AddRangeAsync(installments, cancellationToken);
        }
    }
}