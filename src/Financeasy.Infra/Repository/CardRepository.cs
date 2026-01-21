using System.Linq.Expressions;
using Financeasy.Domain.DTO.Card;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.Repository
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        private readonly FinanceasyDbContext _dbContext;

        public CardRepository(FinanceasyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<GetPagedCardDTO> GetPagedWithRelationsAsync(    
            Expression<Func<Card, bool>> predicate,
            Expression<Func<Card, object>> orderBy,
            bool ascending,
            int page,
            int pageSize)
        {
            IQueryable<Card> query = _dbContext.Cards;

            if ( predicate is not null)
                query = query.Where(predicate);

            var totalItems = await query.CountAsync();

            query = ascending
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);

            return new GetPagedCardDTO
            {   
                List = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new GetCardResponseDTO
                    {
                        Id = x.Id,
                        BankAccountName = x.BankAccount.Bank,
                        Name = x.Name,
                        CreditLimit = x.CreditLimit,
                        AvailableLimit = x.AvailableLimit,
                        ClosingDay = x.ClosingDay,
                        DueDay = x.DueDay
                    })
                    .ToListAsync(),
                TotalItems = totalItems
            };
        }
    }
}