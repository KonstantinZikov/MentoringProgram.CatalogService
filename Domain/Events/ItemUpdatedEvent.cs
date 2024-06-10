using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class ItemUpdatedEvent : BaseEvent
    {
        public ItemUpdatedEvent(Item item) 
        { 
            Item = item;
        }

        public Item Item { get; set; }
    }
}
