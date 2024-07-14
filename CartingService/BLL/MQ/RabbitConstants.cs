namespace Application.MQ
{
    //TODO: move to settings
    public class RabbitConstants
    {   
        public const string ItemUpdatedExchange = "CatalogService.ItemUpdated";

        public const string ItemUpdatedQueue = "CartingService.ItemUpdated";
    }
}
