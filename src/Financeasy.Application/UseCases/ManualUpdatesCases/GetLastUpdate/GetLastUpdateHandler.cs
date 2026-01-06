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
            var lastUpdate = await _updateRepository.GetLastAsync();
            if(lastUpdate is null)
                return null; 

            return new GetLastUpdateResponse { UpdateDate = lastUpdate.UpdateDate, TotalRecurrencesEntrys = lastUpdate.TotalRecurrencesEntrys };
        }
    }
}