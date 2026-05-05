using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces.Services
{
    public interface ICardInstallmentsDapperRepository
    {
        public Task PayAdvanceInstallmentsByPurchaseId(
            Guid purchaseId, int quantity, decimal amountInstallment, CancellationToken cancellationToken
        );

        public Task<List<CardInstallment>?> GetAllInstallmentsUnpaindByPurchaseId(
            Guid purchaseId, CancellationToken cancellationToken
        );
        
        public Task<List<CardInstallment>?> GetAllInstallmentsUnpaindByInvoiceId(
            Guid invoiceId, CancellationToken cancellationToken
        );
    }
}