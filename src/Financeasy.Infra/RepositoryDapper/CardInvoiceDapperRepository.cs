using System.Data;
using System.Data.Common;
using Dapper;
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

            return await connection.QuerySingleAsync(sql);
        }        
    }
}