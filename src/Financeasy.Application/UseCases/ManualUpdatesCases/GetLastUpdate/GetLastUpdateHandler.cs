using Financeasy.Domain.interfaces;
using MediatR;

namespace Financeasy.Application.UseCases.ManualUpdatesCases.GetLastUpdate
{
    public class GetLastUpdateHandler : IRequestHandler<GetLastUpdate, GetLastUpdateResponse>
    {
        private readonly IUpdateRepository _updateRepository;

        public GetLastUpdateHandler(IUpdateRepository updateRepository)
        {
            _updateRepository = updateRepository;
        }

        public async Task<GetLastUpdateResponse> Handle(GetLastUpdate request, CancellationToken cancellationToken)
        {
            var updates = await _updateRepository.GetAllAsync();
            if(updates.Count == 0)
                return null; 

            var lastUpdate = updates.Last();

            return new GetLastUpdateResponse { UpdateDate = lastUpdate.UpdateDate, TotalRecurrencesEntrys = lastUpdate.TotalRecurrencesEntrys };
        }
    }
}