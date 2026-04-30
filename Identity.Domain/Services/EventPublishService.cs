using Identity.Domain.Interfaces.Event;

namespace Identity.Domain.Services;

public class EventPublishService : IEventPublishService
{

    private readonly IRabbitMqPublisher _publisher;

    public EventPublishService(IRabbitMqPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task PublishAsync(string name, string email)
    {
        _publisher.PublishUserCreated(name, email);

        return Task.CompletedTask;
    }
}
