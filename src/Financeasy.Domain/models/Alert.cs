using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Enums;

namespace Financeasy.Domain.models
{
    [Table("alert")]
    public class Alert
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public RecurrenceType RecurrenceType { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime NextDueDate { get; set; }
        public decimal ExpectedAmount { get; set; }

        public Category Category { get; set; }

        public Alert()
        {
        }

        public Alert(Guid userId, Guid categoryId, RecurrenceType recurrenceType, DateTime dueDate, decimal expectedAmount)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CategoryId = categoryId;
            RecurrenceType = recurrenceType;
            DueDate = dueDate;
            NextDueDate = DefineNextDueDate(recurrenceType, dueDate);
            ExpectedAmount = expectedAmount;
        }

        public void UpdateExpectedAmount(decimal newExpectedAmount)
        {
            ExpectedAmount = newExpectedAmount;
        }

        public void Paid()
        {
            var dueDateActual = DueDate;
            DueDate = NextDueDate;
            NextDueDate = DefineNextDueDate(RecurrenceType, dueDateActual);
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