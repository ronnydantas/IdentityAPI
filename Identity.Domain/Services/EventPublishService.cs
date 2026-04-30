using Identity.Domain.DTOs;
using Identity.Domain.Interfaces.Event;

namespace Identity.Domain.Services;

public class EventPublishService : IEventPublishService
{

    private readonly IRabbitMqPublisher _publisher;

    public EventPublishService(IRabbitMqPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task PublishAsync(EventDTO eventDTO)
    {
        _publisher.PublishUserCreated(eventDTO);

        return Task.CompletedTask;
    }
}
