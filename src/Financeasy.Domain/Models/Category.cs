using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Financeasy.Domain.Models.Enums;

namespace Financeasy.Domain.Models
{
    [Table("categorys")]
    public class Category
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("category_type")]
        public CategoryType Type { get; set; }

        [Column("is_fixed")]
        public bool IsFixed { get; set; }

        //navigation
        public User User { get; private set;}
        public List<Transaction> Transactions { get; private set; } = [];

        private Category()
        {
        }

        public Category(string name, Guid userId, CategoryType type, bool isFixed)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Name = name;
            Type = type;
            IsFixed = isFixed;
        }
    }
}