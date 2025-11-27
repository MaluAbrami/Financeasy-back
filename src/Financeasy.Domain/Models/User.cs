using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Financeasy.Domain.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Column("email")]
        [MaxLength(200)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Column("phone_number")]
        [Required]
        [Phone]
        public long PhoneNumber { get; set; }

        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; }

        [Column("password_hash")]
        [Required]
        public string PasswordHash { get; set; }

        public UserSettings Settings { get; set; }

        private User()
        {
        }

        public User(string name, string email, long phoneNumber, string passwordHash)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            CreatedAt = DateTime.UtcNow;
            PasswordHash = passwordHash;
        }
    }
}