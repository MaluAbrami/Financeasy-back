using Financeasy.Domain.DTO.Transaction;

namespace Financeasy.Domain.interfaces
{
    public interface ITransactionDapperRepository
    {
        public Task<GetPagedTransResponseDTO> GetPagedAsync(
            Guid userId,
            string? descriptionFilter,
            string orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken);
    }
}