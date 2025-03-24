 using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Events
{
    public class ConsultaContatoDlqPublisher : IConsultaContatoDlqPublisher
    {
        private const string QUEUE_NAME = "consulta-contato-dlq";
        private readonly ConnectionFactory _factory;

        public ConsultaContatoDlqPublisher()
        {
            var hostname = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
            _factory = new ConnectionFactory() { HostName = hostname };
        }

        public async Task PublicarConsultaVaziaAsync(int? codigoArea)
        {
            await using var connection = await _factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: QUEUE_NAME,
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);
            var mensagem = new
            {
                CodigoArea = codigoArea == null ? 0 : codigoArea.Value,
                Timestamp = DateTime.UtcNow,
                Origem = "contatos-api",
                Mensagem = "Nenhum contato encontrado para a área informada.  - " + codigoArea == null ? "Codigo de area vazio" : codigoArea.Value.ToString(),
            };
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(mensagem));

            var props = new BasicProperties
            {
                ContentType = "application/json",
                DeliveryMode = DeliveryModes.Persistent
            };
            await channel.BasicPublishAsync(exchange: "",
                                            routingKey: QUEUE_NAME,
                                            mandatory: false,
                                            basicProperties: props,
                                            body: body);
        }
    }
}
