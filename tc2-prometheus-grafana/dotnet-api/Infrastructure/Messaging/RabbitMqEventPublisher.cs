using Domain.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Messaging
{
    public class RabbitMqEventPublisher : IEventPublisher
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqEventPublisher()
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public async Task PublishAsync<T>(string queueName, T message, CancellationToken cancellationToken = default)
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queueName,
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null,
                                            cancellationToken: cancellationToken);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            var properties = new BasicProperties
            {
                ContentType = "application/json",
                DeliveryMode = DeliveryModes.Persistent
            };

            await channel.BasicPublishAsync(exchange: "",
                                             routingKey: queueName,
                                             mandatory: false,
                                             properties,
                                             body,
                                             cancellationToken: cancellationToken);
        }
    }
}
