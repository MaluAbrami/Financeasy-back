using System.Data;
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

        public async Task<GetPagedTransResponseDTO> GetPagedAsync(
            Guid userId,
            string? descriptionFilter,
            string orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var connection = _dbContext.Database.GetDbConnection();

            if (connection.State == ConnectionState.Closed)
                await connection.OpenAsync();

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

            var whereClause = "WHERE t.user_id = @UserId";

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
    }
}