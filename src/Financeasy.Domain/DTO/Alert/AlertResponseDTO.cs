namespace Financeasy.Domain.DTO.Alert
{
    public record AlertResponseDTO
    (
        Guid Id,
        string CategoryName,
        decimal ExpectedAmount,
        DateTime DueDate,
        DateTime NextDueDate
    );
}