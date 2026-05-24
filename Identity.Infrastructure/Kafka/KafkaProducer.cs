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
                BootstrapServers =
                    _settings.BootstrapServers,

                SocketTimeoutMs = 5000,

                MessageTimeoutMs = 5000,

                RequestTimeoutMs = 5000,

                Acks = Acks.All
            };

            using var producer =
                new ProducerBuilder<Null, string>(config)
                .Build();

            var message =
                JsonSerializer.Serialize(@event);

            Console.WriteLine(
                $"Enviando mensagem: {message}");

            var result =
                await producer.ProduceAsync(
                    _settings.TopicUserCreated,
                    new Message<Null, string>
                    {
                        Value = message
                    });

            producer.Flush(
                TimeSpan.FromSeconds(5));

            Console.WriteLine(
                $"Mensagem enviada para: {result.TopicPartitionOffset}");
        }
        catch (ProduceException<Null, string> ex)
        {
            Console.WriteLine(
                $"Erro Kafka Produce: {ex.Error.Reason}");

            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Erro geral Kafka: {ex.Message}");

            throw;
        }
    }
}