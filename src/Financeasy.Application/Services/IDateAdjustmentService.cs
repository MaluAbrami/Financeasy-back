using Financeasy.Domain.Enums;

namespace Financeasy.Application.Services
{
    public interface IDateAdjustmentService
    {
        DateTime Adjust(DateTime date, AdjustmentRule rule);
    }
}