using Financeasy.Domain.Enums;

namespace Financeasy.Domain.DTO.Category
{
    public record CreateCategoryRequestDTO
    {
        public string Name { get; set; }
        public EntryType Type { get; set; }
    }
}