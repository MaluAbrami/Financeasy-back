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
                WHERE c.card_id = @CardId AND c.closing_date = @ClosingDate
            ";

            var parameters = new
            {
                CardId = cardId,
                ClosingDate = closingDate
            };

            return await connection.QuerySingleAsync(sql, parameters);
        }       

        public async Task<decimal> GetTotalAmountUnpaidByCardId(
            Guid cardId,
            CancellationToken cancellationToken
        )
        {
            var connection = await GetConnection();

            var sql = $@"
                SELECT COALESCE(SUM(c.total_amount), 0)
                FROM card_invoice c
                WHERE c.card_id = @CardId AND c.is_paid = false
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
                WHERE ci.card_id = @CardId  
                AND ci.due_date = @DueDate
            ";

            var invoice = await connection.QuerySingleAsync(
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
                { "closingDate", "ci.closing_date" },
                { "dueDate", "ci.due_date" }
            };

            if (!allowedColumns.ContainsKey(orderBy))
                orderBy = "purchaseDate";

            var orderColumn = allowedColumns[orderBy];
            var orderDirection = ascending ? "ASC" : "DESC";

            var sql = $@"
                SELECT COUNT(*)
                FROM card_invoice ci
                WHERE ci.card_id = @CardId;

                SELECT
                    ci.id,
                    ci.closing_date AS ClosingDate,
                    ci.due_date AS DueDate,
                    ci.is_paid AS IsPaid,
                    COALESCE(SUM(i.amount),0) AS TotalAmount
                FROM card_invoice ci
                LEFT JOIN card_installment i
                    ON i.card_invoice_id = ci.id
                WHERE ci.card_id = @CardId
                GROUP BY
                    ci.id,
                    ci.closing_date,
                    ci.due_date,
                    ci.is_paid
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