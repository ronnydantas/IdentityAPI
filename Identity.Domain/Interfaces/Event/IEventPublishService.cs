namespace Identity.Domain.Interfaces.Event;

public interface IEventPublishService
{
        Task PublishAsync(string name, string email);
}
