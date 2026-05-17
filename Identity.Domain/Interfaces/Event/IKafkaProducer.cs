using Identity.Domain.DTOs;

namespace Identity.Domain.Interfaces.Event;

public interface IKafkaProducer
{
    Task PublishUserCreated(UserCreatedEvent @event);
}