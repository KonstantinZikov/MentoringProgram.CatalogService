using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Application.MQ
{
    public class RabbitService : IRabbitService
    {
        private readonly ConnectionFactory _connectionFactory;

        public RabbitService(Uri ampqUrl)
        {
            _connectionFactory = new ConnectionFactory
            {
                Uri = ampqUrl
            };
        }

        public Task PublishMessage<TData>(RabbitMessage<TData> message, string exchange, string exchangeType = ExchangeType.Fanout)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            
            channel.ExchangeDeclare(exchange, exchangeType, true);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish(exchange: exchange,
                                 routingKey: string.Empty,
                                 basicProperties: null,
                                 body: body);

            return Task.CompletedTask;
        }
    }
}
