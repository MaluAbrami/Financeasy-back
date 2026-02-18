using System.Linq.Expressions;
using Financeasy.Domain.DTO.Alert;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Infra.Repository
{
    public class AlertRepository : BaseRepository<Alert>, IAlertRepository
    {
        private readonly FinanceasyDbContext _dbContext;
        public AlertRepository(FinanceasyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<GetPagedAlertDTO> GetPagedWithRelationsAsync(    
            Expression<Func<Alert, bool>> predicate,
            Expression<Func<Alert, object>> orderBy,
            bool ascending,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            IQueryable<Alert> query = _dbContext.Alerts;

            if ( predicate is not null)
                query = query.Where(predicate);

            var totalItems = await query.CountAsync();

            query = ascending
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);

            return new GetPagedAlertDTO
            {   
                List = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new AlertResponseDTO(
                        x.Category.Name,
                        x.ExpectedAmount,
                        x.DueDate,
                        x.NextDueDate))
                    .ToListAsync(),
                TotalItems = totalItems
            };
        }
    }
}