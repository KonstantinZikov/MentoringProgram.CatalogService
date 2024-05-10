using Domain.Common;

namespace Domain.ValueObjects
{
    public class Image : ValueObject
    {
        public required string Url { get; init; }

        public string Alt { get; init; } = string.Empty;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Url;
            yield return this.Alt;
        }
    }
}
