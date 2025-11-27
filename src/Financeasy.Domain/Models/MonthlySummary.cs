
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Domain.Models
{
    [Table("monthly_summary")]
    public class MonthlySummary
    {
        [Key]
        [Column("id")]
        public Guid Id { get; private set; }

        [Required]
        [ForeignKey(nameof(User))]
        [Column("user_id")]
        public Guid UserId { get; private set; }

        [Required]
        [Column("year")]
        public int Year { get; private set; }

        [Required]
        [Column("month")]
        public int Month { get; private set; }

        [Precision(18, 2)]
        [Column("total_income")]
        public decimal TotalIncome { get; private set; }

        [Precision(18, 2)]
        [Column("total_expenses")]
        public decimal TotalExpenses { get; private set; }

        [Precision(18, 2)]
        [Column("balance")]
        public decimal Balance { get; private set; }

        [Required]
        [Column(name: "category_breakdown_json", TypeName = "jsonb")]
        public string CategoryBreakdownJson { get; private set; }

        //navigation
        public User User {get; private set;}

        private MonthlySummary() {}

        public MonthlySummary(Guid userId, int year, int month)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Year = year;
            Month = month;
            CategoryBreakdownJson = "{}";
        }
    }
}