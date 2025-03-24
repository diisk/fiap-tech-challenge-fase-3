using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;

namespace ConsultaContatosFunction
{
    public class LerConsultaContatoDlqFunction
    {
        private readonly ILogger _logger;

        public LerConsultaContatoDlqFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LerConsultaContatoDlqFunction>();
        }

        [Function("LerConsultaContatoDlqFunction")]
        public async Task Run([TimerTrigger("*/30 * * * * *")] TimerInfo myTimer)
        {
            var hostname = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "rabbitmq";
            var dlqName = "consulta-contato-dlq";

            _logger.LogInformation($"[DEBUG] RABBITMQ_HOST = {hostname}");
            _logger.LogInformation($">>> Azure Function disparada em: {DateTime.UtcNow:MM/dd/yyyy HH:mm:ss}");

            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = hostname
                };

                _logger.LogInformation("🪝 Aguardando subida do RabbitMQ...");
                await Task.Delay(10000); // Pequena espera para garantir que o RabbitMQ está pronto

                _logger.LogInformation(" Tentando criar conexão com RabbitMQ...");
                await using var connection = await factory.CreateConnectionAsync();
                _logger.LogInformation(" Conexão criada com sucesso.");

                var channel = await connection.CreateChannelAsync();
                _logger.LogInformation(" Canal criado com sucesso.");

                _logger.LogInformation($" Lendo mensagem da fila '{dlqName}'...");

                var result = await channel.BasicGetAsync(dlqName, autoAck: true);

                if (result != null)
                {
                    var body = result.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    _logger.LogInformation($" Mensagem da DLQ recebida: {message}");
                }
                else
                {
                    _logger.LogInformation(" Nenhuma mensagem encontrada na DLQ.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($" Falha ao conectar no RabbitMQ: {ex.Message}");
                _logger.LogError($" StackTrace: {ex.StackTrace}");
            }
        }
    }
}
