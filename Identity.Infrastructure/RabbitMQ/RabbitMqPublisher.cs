using Identity.Domain.DTOs;
using Identity.Domain.Interfaces.Event;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Identity.Infrastructure.RabbitMQ;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly IConfiguration _configuration;

    public RabbitMqPublisher(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void PublishUserCreated(EventDTO eventDTO)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQ:Host"],
            Port = int.Parse(_configuration["RabbitMQ:Port"]!),
            UserName = _configuration["RabbitMQ:Username"],
            Password = _configuration["RabbitMQ:Password"]
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var exchangeName = "user.exchange";

        channel.ExchangeDeclare(
            exchange: exchangeName,
            type: ExchangeType.Fanout,
            durable: true
        );

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(eventDTO));

        channel.BasicPublish(
            exchange: exchangeName,
            routingKey: "",
            basicProperties: null,
            body: body
        );
    }
}