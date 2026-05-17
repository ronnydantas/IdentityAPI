using Confluent.Kafka;
using Identity.Domain.DTOs;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces.Event;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Identity.Infrastructure.Kafka;

public class KafkaProducer : IKafkaProducer
{
    private readonly KafkaSettings _settings;

    public KafkaProducer(IOptions<KafkaSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task PublishUserCreated(
        UserCreatedEvent @event)
    {
        try
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _settings.BootstrapServers
            };

            using var producer =
                new ProducerBuilder<Null, string>(config)
                .Build();

            var message = JsonSerializer.Serialize(@event);

            var result = await producer.ProduceAsync(
                _settings.TopicUserCreated,
                new Message<Null, string>
                {
                    Value = message
                });

            Console.WriteLine(
                $"Mensagem enviada para Kafka. " +
                $"Topic: {result.Topic} | " +
                $"Partition: {result.Partition} | " +
                $"Offset: {result.Offset}");
        }
        catch (ProduceException<Null, string> ex)
        {
            Console.WriteLine(
                $"Erro ao publicar mensagem no Kafka: {ex.Error.Reason}");

            throw new ApplicationException(
                "Erro ao publicar evento no Kafka.",
                ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Erro inesperado no Kafka Producer: {ex.Message}");

            throw new ApplicationException(
                "Erro inesperado ao publicar mensagem no Kafka.",
                ex);
        }
    }
}