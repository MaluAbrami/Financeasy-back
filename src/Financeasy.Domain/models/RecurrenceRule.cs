using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Enums;

namespace Financeasy.Domain.models
{
    [Table("recurrence_rule")]
    public class RecurrenceRule
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("category_id")]
        public Guid CategoryId { get; set; }

        [Column("frequency")]
        public Frequency Frequency { get; set; }

        [Column("day_of_month")]
        public int? DayOfMonth { get; set; }

        [Column("day_of_week")]
        public int? DayOfWeek { get; set; }

        [Column("adjustment_rule")]
        public AdjustmentRule AdjustmentRule { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        public RecurrenceRule()
        {
            
        }

        public RecurrenceRule(Guid categoryId, Frequency frequency, int dayOfMonth, int dayOfWeek, AdjustmentRule adjustmentRule, DateTime startDate, DateTime endDate, decimal amount, bool isActive)
        {
            Id = Guid.NewGuid();
            CategoryId = categoryId;
            Frequency = frequency;
            DayOfMonth = dayOfMonth;
            DayOfWeek = dayOfWeek;
            AdjustmentRule = adjustmentRule;
            StartDate = startDate;
            EndDate = endDate;
            Amount = amount;
            IsActive = isActive;
        }
    }
}