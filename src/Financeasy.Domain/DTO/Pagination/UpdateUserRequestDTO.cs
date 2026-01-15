namespace Financeasy.Domain.DTO.Pagination
{
    public record UpdateUserRequestDTO
    {
        public string? Email { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}