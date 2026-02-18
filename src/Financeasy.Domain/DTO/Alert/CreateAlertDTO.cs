using Financeasy.Domain.Enums;

namespace Financeasy.Domain.DTO.Alert
{
    public record CreateAlertDTO
    (
        Guid CategoryId,
        RecurrenceType RecurrenceType,
        DateTime DueDate,
        decimal ExpectedAmount
    );
}