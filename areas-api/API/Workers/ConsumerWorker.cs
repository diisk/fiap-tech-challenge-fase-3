using API.Consumers;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace API.Workers
{
    public class ConsumerWorker : BackgroundService
    {
        private readonly AreaEventConsumer _areaEventConsumer;

        public ConsumerWorker(AreaEventConsumer areaEventConsumer)
        {
            _areaEventConsumer = areaEventConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _areaEventConsumer.StartConsumingAsync();
        }
    }
}
