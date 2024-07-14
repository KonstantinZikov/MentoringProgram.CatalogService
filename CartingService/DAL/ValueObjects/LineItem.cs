using DAL.Common;

namespace DAL.ValueObjects
{
    public class LineItem : ValueObject
    {
        public required int Id { get; init; }

        public required int ProductId { get; init; }

        public required string Name { get; set; }

        public Image? Image { get; set; }

        public required Money Price { get; set; }

        public required int Quantity { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
