using System.Data;
using System.Data.Common;
using Dapper;
using Financeasy.Domain.DTO.CardInvoice;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.RepositoryDapper
{
    public class CardInvoiceDapperRepository : ICardInvoiceDapperRepository
    {
        private readonly FinanceasyDbContext _dbContext;

        public CardInvoiceDapperRepository(FinanceasyDbContext dbContext)
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

        public async Task<CardInvoice?> GetCardInvoiceByCardIdAndClosingDate(
            Guid cardId,
            DateTime closingDate,
            CancellationToken cancellationToken
        )
        {
            var connection = await GetConnection();

            var sql = $@"
                SELECT *
                FROM card_invoice c
                WHERE c.CardId = @CardId AND c.ClosingDate = @ClosingDate
            ";

            var parameters = new
            {
                CardId = cardId,
                ClosingDate = closingDate
            };

            return await connection.QuerySingleOrDefaultAsync<CardInvoice?>(sql, parameters);
        }       

        public async Task<decimal> GetTotalAmountUnpaidByCardId(
            Guid cardId,
            CancellationToken cancellationToken
        )
        {
            var connection = await GetConnection();

            var sql = $@"
                SELECT COALESCE(SUM(i.Amount),0) AS TotalAmount
                FROM card_invoice c
                LEFT JOIN card_installment i
                    ON i.CardInvoiceId= c.Id
                WHERE c.CardId = @CardId AND c.IsPaid = false
            ";

            var total = await connection.ExecuteScalarAsync<decimal>(
                sql,
                new { CardId = cardId }
            );

            return total;
        } 

        public async Task<CardInvoice?> GetCardInvoiceByPeriod(
            Guid cardId,
            DateTime dueDate,
            CancellationToken cancellationToken
        )
        {
            var connection = await GetConnection();

            var sql = $@"
                SELECT *
                FROM card_invoice ci
                WHERE ci.CardId = @CardId  
                AND ci.DueDate = @DueDate
            ";

            var invoice = await connection.QuerySingleOrDefaultAsync<CardInvoice?>(
                sql,
                new 
                { 
                    CardId = cardId,
                    DueDate = dueDate
                }
            );

            return invoice;
        } 

        public async Task<GetPagedBaseResponseDTO<GetInvoiceResponseDTO>> GetPagedWithRelationsByCardAsync(
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
                { "closingDate", "ci.ClosingDate" },
                { "dueDate", "ci.DueDate" }
            };

            if (!allowedColumns.ContainsKey(orderBy))
                orderBy = "PurchaseDate";

            var orderColumn = allowedColumns[orderBy];
            var orderDirection = ascending ? "ASC" : "DESC";

            var sql = $@"
                SELECT COUNT(*)
                FROM card_invoice ci
                WHERE ci.card_id = @CardId;

                SELECT
                    ci.id,
                    ci.ClosingDate AS ClosingDate,
                    ci.DueDate AS DueDate,
                    ci.IsPaid AS IsPaid,
                    COALESCE(SUM(i.Amount),0) AS TotalAmount
                FROM card_invoice ci
                LEFT JOIN card_installment i
                    ON i.CardInvoiceId = ci.Id
                WHERE ci.CardId = @CardId
                GROUP BY
                    ci.Id,
                    ci.ClosingDate,
                    ci.DueDate,
                    ci.IsPaid
                ORDER BY {orderColumn} {orderDirection}
                LIMIT @PageSize OFFSET @Offset;
            ";

            var parameters = new
            {
                CardId = cardId,
                PageSize = pageSize,
                Offset = (page - 1) * pageSize
            };

            using var multi = await connection.QueryMultipleAsync(sql, parameters);

            var totalItems = await multi.ReadFirstAsync<int>();
            var list = await multi.ReadAsync<GetInvoiceResponseDTO>();

            return new GetPagedBaseResponseDTO<GetInvoiceResponseDTO>
            {
                List = list.ToList(),
                TotalItems = totalItems
            };
        }
    }
}