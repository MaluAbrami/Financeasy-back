using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;

namespace Financeasy.Infra.Repository
{
    public class UpdateRepository : BaseRepository<Update>, IUpdateRepository
    {
        public UpdateRepository(FinanceasyDbContext context) : base(context)
        {
        }
    }
}