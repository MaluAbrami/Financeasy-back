using Financeasy.Domain.DTO.CardInvoice;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface ICardInvoiceDapperRepository
    {
        public Task<CardInvoice?> GetCardInvoiceByCardIdAndClosingDate(
            Guid cardId,
            DateTime closingDate,
            CancellationToken cancellationToken
        );

        public Task<decimal> GetTotalAmountUnpaidByCardId(
            Guid cardId,
            CancellationToken cancellationToken
        );

        public Task<CardInvoice?> GetCardInvoiceByPeriod(
            Guid cardId,
            DateTime dueDate,
            CancellationToken cancellationToken
        );

        public Task<GetPagedBaseResponseDTO<GetInvoiceResponseDTO>> GetPagedWithRelationsByCardAsync(
            Guid cardId,
            string orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken);

        public Task<decimal> GetTotalAmountByCardIdAndPeriod(
            Guid cardId,
            DateTime closingDate,
            CancellationToken cancellationToken
        );
    }
}