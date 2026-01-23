using System.Linq.Expressions;
using Financeasy.Domain.DTO.CardInstallment;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.Repository
{
    public class CardInstallmentRepository : BaseRepository<CardInstallment>, ICardInstallmentRepository
    {
        private readonly FinanceasyDbContext _dbContext;

        public CardInstallmentRepository(FinanceasyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<GetPagedInstallmentsDTO> GetPagedWithRelationsAsync(    
            Expression<Func<CardInstallment, bool>> predicate,
            Expression<Func<CardInstallment, object>> orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            IQueryable<CardInstallment> query = _dbContext.CardInstallments;

            if ( predicate is not null)
                query = query.Where(predicate);

            var totalItems = await query.CountAsync();

            query = ascending
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);

            return new GetPagedInstallmentsDTO
            {   
                List = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new GetInstallmentResponseDTO
                    {
                        Id = x.Id,
                        CategoryName = x.CategoryName,
                        PurchaseDescription = x.CardPurchase.Description!,
                        NumberInstallment = $"{x.Number}/{x.TotalInstallments}",
                        Amount = x.Amount,
                        Paid = x.Paid 
                    })
                    .ToListAsync(),
                TotalItems = totalItems
            };
        }
    }
}