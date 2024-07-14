using DAL.Common;

namespace DAL.ValueObjects
{
    public class Image : ValueObject
    {
        public required string Url { get; init; }

        public string? Alt { get; init; } = string.Empty;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Url;
            yield return Alt;
        }
    }
}
