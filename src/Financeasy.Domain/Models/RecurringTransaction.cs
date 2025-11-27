using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Models.Enums;

namespace Financeasy.Domain.Models
{
    [Table("recurring_transaction")]
    public class RecurringTransaction
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("user_id")]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        [Column("category_id")]
        public Guid CategoryId { get; set; }

        [Required]
        [Column("frequency")]
        public Frequency Frequency { get; set; }

        [Required]
        [Column("amount")]
        public decimal Amount { get; set; }

        [Required]
        [Column("start_date")]
        public DateOnly StartDate { get; set; }

        [Column("end_date")]
        public DateOnly? EndDate { get; set; }

        //navigation
        public List<Transaction> Transactions { get; private set; } = [];

        private RecurringTransaction()
        {
        }

        public RecurringTransaction(Guid userId, Guid categoryId, Frequency frequency, decimal amount, DateOnly startDate, DateOnly? endDate)
        {
            Id = Guid.NewGuid();
            CategoryId = categoryId;
            Frequency = frequency;
            Amount = amount;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}