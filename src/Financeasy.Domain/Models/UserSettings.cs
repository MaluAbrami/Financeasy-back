using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Financeasy.Domain.Models
{
    [Table("user_settings")]
    public class UserSettings
    {
        [Key]
        public Guid Id { get; set; }

        [Column("user_id")]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Column("is_fixed_salary")]
        [Required]
        public SalaryType SalaryType { get; set; }

        [Column("liquid_salary")]
        [Precision(18,2)]
        public decimal? LiquidSalary { get; set; }

        //navigation
        public User User { get; private set; }

        private UserSettings()
        {
        }

        public UserSettings(Guid userId, SalaryType salaryType, decimal? liquidSalary)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            SalaryType = salaryType;
            LiquidSalary = liquidSalary;
        }
    }
}