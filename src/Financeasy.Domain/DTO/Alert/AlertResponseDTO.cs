namespace Financeasy.Domain.DTO.Alert
{
    public record AlertResponseDTO
    (
        string CategoryName,
        decimal ExpectedAmount,
        DateTime DueDate,
        DateTime NextDueDate
    );
}