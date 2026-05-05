using Financeasy.Domain.DTO.CardInvoice;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.models;

namespace Financeasy.Domain.interfaces
{
    public interface ICardInvoiceDapperRepository
    {
        Task<CardInvoice?> GetCardInvoiceByCardIdAndClosingDate(
            Guid cardId,
            DateTime closingDate,
            CancellationToken cancellationToken
        );

        Task<decimal> GetTotalAmountUnpaidByCardId(
            Guid cardId,
            CancellationToken cancellationToken
        );

        Task<CardInvoice?> GetCardInvoiceByPeriod(
            Guid cardId,
            DateTime dueDate,
            CancellationToken cancellationToken
        );

        Task<GetPagedBaseResponseDTO<GetInvoiceResponseDTO>> GetPagedWithRelationsByCardAsync(
            Guid cardId,
            string orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken);

        Task<decimal> GetTotalAmountByCardIdAndPeriod(
            Guid cardId,
            DateTime closingDate,
            CancellationToken cancellationToken
        );

        Task<List<CardInvoice>?> GetAllInvoicesActiveByCardId(
            Guid cardId,
            DateTime today,
            CancellationToken cancellationToken
        );
    }
}