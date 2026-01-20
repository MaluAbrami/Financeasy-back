using System.Linq.Expressions;
using Financeasy.Domain.DTO.CardPurchase;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.Repository
{
    public class CardPurchaseRepository : BaseRepository<CardPurchase>, ICardPurchaseRepository
    {
        private readonly FinanceasyDbContext _dbContext;

        public CardPurchaseRepository(FinanceasyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<GetPagedCardPurchaseDTO> GetPagedWithRelationsAsync(    
            Expression<Func<CardPurchase, bool>> predicate,
            Expression<Func<CardPurchase, object>> orderBy,
            bool ascending,
            int page,
            int pageSize)
        {
            IQueryable<CardPurchase> query = _dbContext.CardPurchases;

            if ( predicate is not null)
                query = query.Where(predicate);

            var totalItems = await query.CountAsync();

            query = ascending
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);

            return new GetPagedCardPurchaseDTO
            {   
                List = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new GetCardPurchaseResponseDTO
                    {
                        CardName = x.Card!.Name,
                        CategoryName = x.Category.Name,
                        TotalAmount = x.TotalAmount,
                        Installments = x.Installments,
                        PurchaseDate = x.PurchaseDate,
                        Description = x.Description
                    })
                    .ToListAsync(),
                TotalItems = totalItems
            };
        }
    }
}