using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Domain.Models
{
    [Table("transction")]
    public class Transction
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Column("user_id")]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required]
        [Column("category_id")]
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }

        [Required]
        [Column("amount")]
        [Precision(18,2)]
        public decimal Amount { get; set; }

        [Required]
        [Column("date")]
        public DateOnly Date { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("recurrence_id")]
        public Guid? RecurrenceId { get; set; }

        //navigation
        public User User {get; private set;}
        public Category Category {get; private set;}
        public ReccuringTransction ReccuringTransction {get; private set;}

        private Transction()
        {
        }

        public Transction(Guid userId, Guid categoryId, decimal amount, DateOnly date, string? description, Guid? recurrenceId)
        {
            Id = Guid.NewGuid();
            CategoryId = categoryId;
            Amount = amount;
            Date = date;
            Description = description;
            RecurrenceId = recurrenceId;
        }
    }
}