using DAL.Common;
using DAL.ValueObjects;

namespace DAL.Entities
{
    public class Cart : BaseAuditableEntity<string>
    {
        public int LinesIdCounter { get; set; }

        public IList<LineItem> Items { get; private set; } = new List<LineItem>();
    }
}
