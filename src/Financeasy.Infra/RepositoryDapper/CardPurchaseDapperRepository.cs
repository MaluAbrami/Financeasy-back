using System.Data;
using System.Data.Common;
using Dapper;
using Financeasy.Domain.DTO.CardPurchase;
using Financeasy.Domain.interfaces;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.RepositoryDapper
{
    public class CardPurchaseDapperRepository : ICardPurchaseDapperRepository
    {
        private readonly FinanceasyDbContext _dbContext;

        public CardPurchaseDapperRepository(FinanceasyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private async Task<DbConnection> GetConnection()
        {
            var connection = _dbContext.Database.GetDbConnection();

            if (connection.State == ConnectionState.Closed)
                await connection.OpenAsync();

            return connection;
        }        

        public async Task<GetPagedCardPurchaseDTO> GetPagedWithRelationsAsync(
            Guid userId,
            string orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var connection = await GetConnection();

            // whitelist para evitar SQL injection
            var allowedColumns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "purchaseDate", "cp.purchase_date" },
                { "totalAmount", "cp.total_amount" },
                { "description", "cp.description" }
            };

            if (!allowedColumns.ContainsKey(orderBy))
                orderBy = "purchaseDate";

            var orderColumn = allowedColumns[orderBy];
            var orderDirection = ascending ? "ASC" : "DESC";

            var sql = $@"
                SELECT COUNT(*)
                FROM card_purchase cp
                WHERE cp.user_id = @UserId;

                SELECT
                    cp.id,
                    c.name AS CardName,
                    cat.name AS CategoryName,
                    cp.total_amount AS TotalAmount,
                    cp.installments,
                    cp.purchase_date AS PurchaseDate,
                    cp.description
                FROM card_purchase cp
                INNER JOIN card c ON c.id = cp.card_id
                INNER JOIN category cat ON cat.id = cp.category_id
                WHERE cp.user_id = @UserId
                ORDER BY {orderColumn} {orderDirection}
                LIMIT @PageSize OFFSET @Offset;
            ";

            var parameters = new
            {
                UserId = userId,
                PageSize = pageSize,
                Offset = (page - 1) * pageSize
            };

            using var multi = await connection.QueryMultipleAsync(sql, parameters);

            var totalItems = await multi.ReadFirstAsync<int>();
            var list = await multi.ReadAsync<GetCardPurchaseResponseDTO>();

            return new GetPagedCardPurchaseDTO
            {
                List = list.ToList(),
                TotalItems = totalItems
            };
        }
    }
}