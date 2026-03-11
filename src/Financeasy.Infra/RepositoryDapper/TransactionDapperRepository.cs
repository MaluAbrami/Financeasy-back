using System.Data;
using System.Data.Common;
using System.Transactions;
using Dapper;
using Financeasy.Domain.DTO.Transaction;
using Financeasy.Domain.interfaces;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.RepositoryDapper
{
    public class TransactionDapperRepository : ITransactionDapperRepository
    {
        private readonly FinanceasyDbContext _dbContext;

        public TransactionDapperRepository(FinanceasyDbContext dbContext)
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

        public async Task<GetPagedTransResponseDTO> GetPagedAsync(
            Guid userId,
            string? descriptionFilter,
            string orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var connection = await GetConnection();

            // 🔒 WHITELIST para evitar SQL Injection
            var allowedColumns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "date", "t.Date" },
                { "amount", "t.Amount" },
                { "description", "t.Description" }
            };

            if (!allowedColumns.ContainsKey(orderBy))
                orderBy = "date";

            var orderColumn = allowedColumns[orderBy];
            var orderDirection = ascending ? "ASC" : "DESC";

            var whereClause = "WHERE t.UserId = @UserId";

            if (!string.IsNullOrEmpty(descriptionFilter))
                whereClause += " AND t.description LIKE @Description";

            var sql = $@"
                SELECT COUNT(*)
                FROM transaction t
                {whereClause};

                SELECT 
                    t.Id,
                    ba.BankName AS BankAccountName,
                    c.Name AS CategoryName,
                    t.PaymentMethod,
                    t.Amount,
                    t.Date,
                    t.Description
                FROM transaction t
                INNER JOIN bank_account ba ON ba.Id = t.BankAccountId
                INNER JOIN category c ON c.Id = t.CategoryId
                {whereClause}
                ORDER BY {orderColumn} {orderDirection}
                LIMIT @PageSize OFFSET @Offset;";

            var parameters = new
            {
                UserId = userId,
                Description = descriptionFilter != null ? $"%{descriptionFilter}%" : null,
                PageSize = pageSize,
                Offset = (page - 1) * pageSize
            };

            using var multi = await connection.QueryMultipleAsync(sql, parameters);

            var totalItems = await multi.ReadFirstAsync<int>();
            var list = await multi.ReadAsync<GetTransactionResponseDTO>();

            return new GetPagedTransResponseDTO
            {
                List = (List<GetTransactionResponseDTO>)list,
                TotalItems = totalItems
            };
        }

        public async Task<decimal> GetTotalBalanceMonthlyExpense(Guid userId, int month, int year, CancellationToken cancellationToken)
        {
            var connection = await GetConnection();

            var startDate = new DateTime(year, month, 1);

            var sql = $@"
                SELECT COALESCE(SUM(t.Amount), 0)
                FROM transaction t
                INNER JOIN category c ON c.Id = t.CategoryId
                WHERE t.UserId = @UserId
                AND t.Date >= @StartDate
                AND t.Date < @EndDate
                AND c.Type = 'Expense';
            ";

            var parameters = new
            {
                UserId = userId,
                StartDate = startDate,
                EndDate = startDate.AddMonths(1)
            };

            var total = await connection.QuerySingleAsync<decimal>(sql, parameters);

            return total;
        }

        public async Task<decimal> GetTotalBalanceMonthlyIncome(Guid userId, int month, int year, CancellationToken cancellationToken)
        {
            var connection = await GetConnection();

            var startDate = new DateTime(year, month, 1);

            var sql = $@"
                SELECT COALESCE(SUM(t.Amount), 0)
                FROM transaction t
                INNER JOIN category c ON c.Id = t.CategoryId
                WHERE t.UserId = @UserId
                AND t.Date >= @StartDate
                AND t.Date < @EndDate
                AND c.Type = 'Income';
            ";

            var parameters = new
            {
                UserId = userId,
                StartDate = startDate,
                EndDate = startDate.AddMonths(1)
            };

            var total = await connection.QuerySingleAsync<decimal>(sql, parameters);

            return total;
        }
    }
}