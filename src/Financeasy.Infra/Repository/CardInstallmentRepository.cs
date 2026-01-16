using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using Financeasy.Infra.Persistence;

namespace Financeasy.Infra.Repository
{
    public class CardInstallmentRepository : BaseRepository<CardInstallment>, ICardInstallmentRepository
    {
        public CardInstallmentRepository(FinanceasyDbContext context) : base(context)
        {
        }
    }
}