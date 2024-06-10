using Application.MQ;
using AutoMapper;
using Domain.Events;
using MediatR;

namespace Application.Items.EventHandlers;

public class ItemUpdatedRabbitEventHandler : INotificationHandler<ItemUpdatedEvent>
{
    private readonly IRabbitService _rabbitMQService;

    private readonly IMapper _mapper;

    public ItemUpdatedRabbitEventHandler(IMapper mapper, IRabbitService rabbitMQService)
    {
        _rabbitMQService = rabbitMQService;
        _mapper = mapper;
    }

    public Task Handle(ItemUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var message = new RabbitMessage<ItemDto> { Data = _mapper.Map<ItemDto>(notification.Item) };
        _rabbitMQService.PublishMessage(message, RabbitConstants.ItemUpdatedExchange);

        return Task.CompletedTask;
    }
}
