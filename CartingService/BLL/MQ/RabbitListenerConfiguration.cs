namespace BLL.MQ
{
    public record RabbitListenerConfiguration
    {
        public required Uri RabbitUrl { get; init; }
    }
}
