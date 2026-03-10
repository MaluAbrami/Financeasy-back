using System.Data;
using System.Data.Common;
using Dapper;
using Financeasy.Domain.interfaces.Services;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.RepositoryDapper
{
    public class CardInstallmentsDapperRepository : ICardInstallmentsDapperRepository
    {
        private readonly FinanceasyDbContext _dbContext;

        public CardInstallmentsDapperRepository(FinanceasyDbContext dbContext)
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

        public async Task PayAdvanceInstallmentsByPurchaseId(
            Guid purchaseId, int quantity, decimal amountInstallment, CancellationToken cancellationToken
        )
        {
            var connection = await GetConnection();

            var sql = $@"
                UPDATE card_installment i
                SET i.Paid = true,
                    i.Amount = @AmountInstallment
                WHERE i.CardPurchaseId = @PurchaseId
                AND i.Paid = false
                LIMIT @Quantity;
            ";

            var parameters = new
            {
                PurchaseId = purchaseId,
                Quantity = quantity,
                AmountInstallment = amountInstallment
            };

            await connection.ExecuteAsync(sql, parameters);
        }

        public async Task<List<CardInstallment>?> GetAllInstallmentsUnpaindByPurchaseId(
            Guid purchaseId, CancellationToken cancellationToken
        )
        {
            var connection = await GetConnection();

            var sql = $@"
                SELECT *
                FROM card_installment i
                WHERE i.CardPurchaseId = @PurchaseId AND i.Paid = false;
            ";

            var parameters = new
            {
                PurchaseId = purchaseId
            };

            return await connection.QuerySingleOrDefaultAsync(sql, parameters);
        }

        public async Task<List<CardInstallment>?> GetAllInstallmentsUnpaindByInvoiceId(
            Guid invoiceId, CancellationToken cancellationToken
        )
        {
            var connection = await GetConnection();

            var sql = $@"
                SELECT *
                FROM card_installment i
                WHERE i.CardInvoiceId = @InvoiceId AND i.Paid = false;
            ";

            var parameters = new
            {
                InvoiceId = invoiceId
            };

            return await connection.QuerySingleOrDefaultAsync(sql, parameters);
        }
    }
}