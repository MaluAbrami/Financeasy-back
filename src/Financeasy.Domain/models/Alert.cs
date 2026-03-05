using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Enums;

namespace Financeasy.Domain.models
{
    [Table("alert")]
    public class Alert
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
        [Column("category_id")]
        public Guid CategoryId { get; set; }
        [Column("recurrence_type")]
        public RecurrenceType RecurrenceType { get; set; }
        [Column("due_date")]
        public DateTime DueDate { get; set; }
        [Column("next_due_date")]
        public DateTime NextDueDate { get; set; }
        [Column("expected_amount")]
        public decimal ExpectedAmount { get; set; }
        [Column("start_date")]
        public DateTime? StartDate { get; set; }
        [Column("end_date")]
        public DateTime? EndDate { get; set; }

        public Category Category { get; set; }

        public Alert()
        {
        }

        public Alert(Guid userId, Guid categoryId, RecurrenceType recurrenceType, DateTime dueDate, decimal expectedAmount, DateTime? startDate, DateTime? endDate)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CategoryId = categoryId;
            RecurrenceType = recurrenceType;
            DueDate = dueDate;
            NextDueDate = DefineNextDueDate(recurrenceType, dueDate);
            ExpectedAmount = expectedAmount;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void UpdateExpectedAmount(decimal newExpectedAmount)
        {
            ExpectedAmount = newExpectedAmount;
        }

        public void Paid()
        {
            DueDate = NextDueDate;
            NextDueDate = DefineNextDueDate(RecurrenceType, DueDate);
        }

        private DateTime DefineNextDueDate(RecurrenceType recurrenceType, DateTime dueDate)
        {
            return recurrenceType switch
            {
                RecurrenceType.None => dueDate,
                RecurrenceType.Fortnightly => dueDate.AddDays(15),
                RecurrenceType.Monthly => dueDate.AddMonths(1),
                RecurrenceType.Quarterly => dueDate.AddMonths(3),
                RecurrenceType.Semiannul => dueDate.AddMonths(6),
                RecurrenceType.Annual => dueDate.AddYears(1),
                _ => dueDate
            };
        }
    }
}