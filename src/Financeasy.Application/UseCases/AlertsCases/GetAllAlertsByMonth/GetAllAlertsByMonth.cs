using Financeasy.Domain.DTO.Alert;
using Financeasy.Domain.DTO.Pagination;
using MediatR;

namespace Financeasy.Application.UseCases.AlertsCases.GetAllAlertsByMonth
{
    public record GetAllAlertsByMonthResponse
    (
        List<AlertResponseDTO> Alerts,
        PaginationResponseBase Pagination
    );

    public record GetAllAlertsByMonth : IRequest<GetAllAlertsByMonthResponse>
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public Guid UserId { get; set; }
        public PaginationRequestBase Pagination { get; set; }
    }
}