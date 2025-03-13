using Domain.Entities;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace API.Consumers
{
    public class AreaEventConsumer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AreaEventConsumer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartConsumingAsync()
        {
            var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")??"Development";
            var factory = new ConnectionFactory() { HostName = enviroment.ToUpper()=="DEVELOPMENT"?"localhost":"host.docker.internal" };

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "AreaAtualizadaQueue",
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var area = JsonSerializer.Deserialize<Area>(message);

                if (area != null)
                {
                    using var scope = _scopeFactory.CreateScope();
                    var readDbContext = scope.ServiceProvider.GetRequiredService<OnlyReadDbContext>();
                    var writeDbContext = scope.ServiceProvider.GetRequiredService<OnlyWriteDbContext>();

                    var existingArea = await readDbContext.AreaSet.AsNoTracking()
                        .FirstOrDefaultAsync(a => a.Codigo == area.Codigo);

                    if (existingArea == null)
                    {
                        await writeDbContext.AreaSet.AddAsync(area);
                    }
                    else
                    {
                        var trackedArea = await writeDbContext.AreaSet.FirstOrDefaultAsync(a => a.Codigo == area.Codigo);
                        if (trackedArea != null)
                        {
                            writeDbContext.Entry(trackedArea).CurrentValues.SetValues(area);
                        }
                    }

                    await writeDbContext.SaveChangesAsync();
                }
            };

            await channel.BasicConsumeAsync(queue: "AreaAtualizadaQueue",
                                            autoAck: true,
                                            consumer: consumer);
        }
    }
}