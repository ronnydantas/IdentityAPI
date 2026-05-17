using Identity.Domain.DTOs;

namespace Identity.Domain.Interfaces.Event;

public interface IRabbitMqPublisher
{
    void PublishUserCreated(UserCreatedEvent eventDTO);
}
