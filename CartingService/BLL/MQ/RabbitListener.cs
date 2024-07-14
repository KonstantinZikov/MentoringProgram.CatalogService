using RabbitMQ.Client;

namespace BLL.MQ
{
    public abstract class RabbitListener : IRabbitListener
    {
        private readonly ConnectionFactory _factory;

        private IConnection? _connection;

        private IModel? _channel;

        public RabbitListener(RabbitListenerConfiguration configuration)
        {
            _factory = new ConnectionFactory() { Uri = configuration.RabbitUrl };
        }

        public void Start()
        {
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            Listen(_connection, _channel);
        }

        protected abstract void Listen(IConnection connection, IModel channel);

        public void Stop()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
