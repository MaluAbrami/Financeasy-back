using Financeasy.Domain.models;

namespace Financeasy.Application.Services
{
    public interface IUpdateExecutionService
    {
        Task<Update> ExecuteAsync(Guid userId, DateTime executionDate, CancellationToken ct);
    }
}