using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Financeasy.Domain.models
{
    [Table("users")]
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? ProfilePhoto { get; set; } = string.Empty;
        public decimal AlertLimit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public User()
        {
        }

        public User(string email, string passwordHash, string? profilePhoto, decimal alertLimit)
        {
            Id = Guid.NewGuid();
            Email = email;
            PasswordHash = passwordHash;
            ProfilePhoto = profilePhoto;
            AlertLimit = alertLimit;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
        }
    }
}