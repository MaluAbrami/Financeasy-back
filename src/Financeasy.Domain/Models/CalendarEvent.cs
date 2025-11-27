using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using Financeasy.Domain.Models.Enums;

namespace Financeasy.Domain.Models
{
    [Table("calendar_events")]
    public class CalendarEvent
    {
        [Key]
        [Column("id")]
        public Guid Id { get; private set; }

        [Required]
        [ForeignKey(nameof(User))]
        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Required]
        [MaxLength(200)]
        [Column("title")]
        public string Title { get; private set; }

        [Required]
        [Column("date")]
        public DateTime Date { get; private set; }

        [Column("time")]
        public TimeSpan? Time { get; private set; }

        [Required]
        [MaxLength(20)]
        [Column("type")]
        public CalendarEventType Type { get; private set; }

        [Column("transaction_id")]
        public Guid? TransactionId { get; private set; }

        // Navigation
        public User User { get; private set; }
        public Transaction Transaction { get; private set; }

        private CalendarEvent() { }

        public CalendarEvent(Guid userId, string title, DateTime date, TimeSpan? time, CalendarEventType type)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Title = title;
            Date = date;
            Time = time;
            Type = type;
        }
    }
}