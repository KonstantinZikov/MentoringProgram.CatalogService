using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Item : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public Image? Image { get; set; }

        public required Category Category { get; set; }

        public required Money Price { get; set; }

        public required int Amount { get; set; }     
    }
}
