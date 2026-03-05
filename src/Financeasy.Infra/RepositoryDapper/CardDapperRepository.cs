using System.Data;
using System.Data.Common;
using Dapper;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.RepositoryDapper
{
    public class CardDapperRepository : ICardDapperRepository
    {
        private readonly FinanceasyDbContext _dbContext;

        public CardDapperRepository(FinanceasyDbContext dbContext)
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

        public async Task<Card?> GetCardById(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var connection = await GetConnection();

            var sql = $@"
                SELECT *
                FROM card c
                WHERE c.id = @Id
            ";

            return await connection.QuerySingleAsync(sql);
        }
    }
}