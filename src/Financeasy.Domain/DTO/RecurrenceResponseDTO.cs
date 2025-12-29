using Financeasy.Domain.Enums;

namespace Financeasy.Domain.DTO
{
    public record RecurrenceResponseDTO
    {
        public Guid Id { get; set; }
        public Frequency Frequency { get; set; }
        public int? DayOfMonth { get; set; }
        public int? DayOfWeek { get; set; }
        public AdjustmentRule AdjustmentRule { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
    }
}