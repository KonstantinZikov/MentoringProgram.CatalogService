using Application.MQ;
using BLL.Carts.Commands;
using BLL.MQ;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BLL.Carts.Listeners
{
    internal class ItemUpdatedRabbitListener : RabbitListener
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ItemUpdatedRabbitListener(
            IServiceScopeFactory serviceScopeFactory,
            RabbitListenerConfiguration configuration) : base(configuration)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        private record UpdatedItemDto()
        {
            public int Id { get; init; }

            public required string Name { get; set; }

            public string? Description { get; set; }

            public string? ImageUrl { get; init; }

            public decimal Price { get; set; }

            public required string PriceCurrency { get; set; }
        }

        protected override void Listen(IConnection connection, IModel channel)
        {
            channel.ExchangeDeclare(RabbitConstants.ItemUpdatedExchange, ExchangeType.Fanout, true);
            channel.QueueDeclare(RabbitConstants.ItemUpdatedQueue, true, false, false);
            channel.QueueBind(RabbitConstants.ItemUpdatedQueue, RabbitConstants.ItemUpdatedExchange, string.Empty);
            
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, deliveryEventArgs) =>
            {
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {
                    ISender sender = scope.ServiceProvider.GetService<ISender>();

                    var body = deliveryEventArgs.Body.ToArray();
                    RabbitMessage<UpdatedItemDto> message = JsonSerializer.Deserialize<RabbitMessage<UpdatedItemDto>>(Encoding.UTF8.GetString(body));
                    UpdatedItemDto item = message.Data;

                    await sender.Send(new UpdateItemInCartsCommand
                    {
                        ProductId = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        PriceCurrency = item.PriceCurrency,
                        ImageUrl = item.ImageUrl
                    });

                    channel.BasicAck(deliveryEventArgs.DeliveryTag, false);
                }                
            };

            channel.BasicConsume(RabbitConstants.ItemUpdatedQueue, false, consumer);
        }
    }
}
