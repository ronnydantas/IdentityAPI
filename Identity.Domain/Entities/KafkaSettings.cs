namespace Identity.Domain.Entities;

public class KafkaSettings
{
    public string BootstrapServers { get; set; } = string.Empty;
    public string TopicUserCreated { get; set; } = string.Empty;
}
