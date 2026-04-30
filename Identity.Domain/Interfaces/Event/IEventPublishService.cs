using Identity.Domain.DTOs;

namespace Identity.Domain.Interfaces.Event;

public interface IEventPublishService
{
        Task PublishAsync(EventDTO eventDTO);
}
