using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Financeasy.Domain.models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [Column("password")]
        public string PasswordHash { get; set; }

        [Column("profile_photo")]
        public string? ProfilePhoto { get; set; }

        [Column("alert_limit")]
        public decimal AlertLimit { get; set; }

        public User()
        {
        }

        public User(string email, string passwordHash, string? profilePhoto)
        {
            Id = Guid.NewGuid();
            Email = email;
            PasswordHash = passwordHash;

            if(profilePhoto is not null)
                ProfilePhoto = profilePhoto;
        }
    }
}