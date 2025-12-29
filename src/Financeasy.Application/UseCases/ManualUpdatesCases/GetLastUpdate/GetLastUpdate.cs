using MediatR;

namespace Financeasy.Application.UseCases.ManualUpdatesCases.GetLastUpdate
{
    public record GetLastUpdateResponse()
    {
        public DateTime UpdateDate { get; set; }
        public int TotalRecurrencesEntrys { get; set; }
    }

    public record GetLastUpdate : IRequest<GetLastUpdateResponse>
    {
        public Guid UserId { get; set; }
    }
}