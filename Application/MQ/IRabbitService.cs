using RabbitMQ.Client;

namespace Application.MQ
{
    public interface IRabbitService
    {
        Task PublishMessage<TData>(RabbitMessage<TData> eventToPublish, string exchange, string exchangeType = ExchangeType.Fanout);
    }
}
