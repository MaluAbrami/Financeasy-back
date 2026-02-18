using Financeasy.Domain.DTO.Alert;
using Financeasy.Domain.DTO.Pagination;
using Financeasy.Domain.interfaces;
using Financeasy.Domain.models;
using MediatR;

namespace Financeasy.Application.UseCases.AlertsCases.GetAllAlertsByMonth
{
    public class GetAllAlertsByMonthHandler : IRequestHandler<GetAllAlertsByMonth, GetAllAlertsByMonthResponse>
    {
        private readonly IAlertRepository _alertRepository;

        public GetAllAlertsByMonthHandler(
            IAlertRepository alertRepository
        )
        {
            _alertRepository = alertRepository;
        }

        public async Task<GetAllAlertsByMonthResponse> Handle(GetAllAlertsByMonth request, CancellationToken cancellationToken)
        {
            var start = new DateTime(request.Year, request.Month, 1);
            var end = start.AddMonths(1);

            var alerts = await _alertRepository.GetPagedWithRelationsAsync(
                x => x.UserId == request.UserId
                     && x.DueDate >= start
                     && x.DueDate < end,
                x => x.DueDate,
                false,
                request.Pagination.Page,
                request.Pagination.PageSize,
                cancellationToken
            );

            List<AlertResponseDTO> listResponse = [];
            foreach (var alert in alerts.List)
            {
                var alertResponse = new AlertResponseDTO
                (
                    alert.Id,
                    alert.CategoryName,
                    alert.ExpectedAmount,
                    alert.DueDate,
                    alert.NextDueDate
                );

                listResponse.Add(alertResponse);
            }

            return new GetAllAlertsByMonthResponse
            (
                listResponse,
                new PaginationResponseBase
                {
                    Page = request.Pagination.Page,
                    PageSize = request.Pagination.PageSize,
                    TotalItems = alerts.TotalItems,
                    TotalPages = (int)Math.Ceiling(alerts.TotalItems / (double)request.Pagination.PageSize)
                }
            );
        }
    }
}