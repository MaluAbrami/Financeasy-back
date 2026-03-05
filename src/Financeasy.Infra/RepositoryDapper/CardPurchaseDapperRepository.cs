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
                { "purchaseDate", "cp.PurchaseDate" },
                { "totalAmount", "cp.TotalAmount" },
                { "description", "cp.Description" }
            };

            if (!allowedColumns.ContainsKey(orderBy))
                orderBy = "purchaseDate";

            var orderColumn = allowedColumns[orderBy];
            var orderDirection = ascending ? "ASC" : "DESC";

            var sql = $@"
                SELECT COUNT(*)
                FROM card_purchase cp
                WHERE cp.UserId = @UserId;

                SELECT
                    cp.Id,
                    c.Name AS CardName,
                    cat.Name AS CategoryName,
                    cp.TotalAmount AS TotalAmount,
                    cp.Installments,
                    cp.PurchaseDate AS PurchaseDate,
                    cp.Description
                FROM card_purchase cp
                INNER JOIN card c ON c.Id = cp.CardId
                INNER JOIN category cat ON cat.Id = cp.CategoryId
                WHERE cp.UserId = @UserId
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

        public async Task<GetPagedCardPurchaseDTO> GetPagedWithRelationsByCardAsync(
            Guid cardId,
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
                { "purchaseDate", "cp.PurchaseDate" },
                { "totalAmount", "cp.TotalAmount" },
                { "description", "cp.Description" }
            };

            if (!allowedColumns.ContainsKey(orderBy))
                orderBy = "purchaseDate";

            var orderColumn = allowedColumns[orderBy];
            var orderDirection = ascending ? "ASC" : "DESC";

            var sql = $@"
                SELECT COUNT(*)
                FROM card_purchase cp
                WHERE cp.CardId = @CardId;

                SELECT
                    cp.Id,
                    c.Name AS CardName,
                    cat.Name AS CategoryName,
                    cp.TotalAmount AS TotalAmount,
                    cp.Installments,
                    cp.PurchaseDate AS PurchaseDate,
                    cp.Description
                FROM card_purchase cp
                INNER JOIN card c ON c.id = cp.CardId
                INNER JOIN category cat ON cat.id = cp.CategoryId
                WHERE cp.CardId = @CardId
                ORDER BY {orderColumn} {orderDirection}
                LIMIT @PageSize OFFSET @Offset;
            ";

            var parameters = new
            {
                Cardid = cardId,
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