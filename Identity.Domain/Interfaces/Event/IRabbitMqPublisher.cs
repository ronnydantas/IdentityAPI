using Identity.Domain.DTOs;

namespace Identity.Domain.Interfaces.Event;

public interface IRabbitMqPublisher
{
    void PublishUserCreated(EventDTO eventDTO);
}
