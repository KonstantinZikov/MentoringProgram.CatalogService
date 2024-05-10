using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Category : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }

        public Image? Image { get; set; }

        public Category? ParentCategory { get; set; }

    }
}
