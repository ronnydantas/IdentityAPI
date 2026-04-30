namespace Identity.Domain.Interfaces.Event;

public interface IRabbitMqPublisher
{
    void PublishUserCreated(string name, string email);
}
