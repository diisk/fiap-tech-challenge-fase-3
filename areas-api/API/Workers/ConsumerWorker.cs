using API.Consumers;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace API.Workers
{
    public class ConsumerWorker : BackgroundService
    {
        private readonly UsuarioAtualizadoEventConsumer usuarioAtualizadoEventConsumer;

        public ConsumerWorker(UsuarioAtualizadoEventConsumer usuarioAtualizadoEventConsumer)
        {
            this.usuarioAtualizadoEventConsumer = usuarioAtualizadoEventConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await usuarioAtualizadoEventConsumer.StartConsumingAsync();
        }
    }
}
