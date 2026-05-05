namespace Financeasy.Domain.DTO.User
{
    public record UpdateUserRequestDTO
    {
        public string? Email { get; set; }
        public string? ProfilePhoto { get; set; }
        public decimal AlertLimit { get; set; }
    }
}