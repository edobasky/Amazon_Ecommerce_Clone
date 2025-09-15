
using System.Text.Json;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Ordering.Data;

namespace Ordering.Dispatcher
{
    public class OutboxMessageDispatcher(IServiceProvider serviceProvider, ILogger<OutboxMessageDispatcher> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService <OrderContext>();
                var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                var pendingMessages = await dbContext.outboxMessages
                                            .Where(x => x.ProcessedOn == null)
                                            .OrderBy(x => x.CreatedDate)
                                            .Take(20)
                                            .ToListAsync();

                foreach (var message in pendingMessages)
                {
                    try
                    {
                        // var dynamicData = JsonSerializer.Deserialize<dynamic>(message.Content);
                        var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message.Content);
                        await publishEndpoint.Publish(orderCreatedEvent);

                        message.ProcessedOn = DateTime.UtcNow;
                        logger.LogInformation("Published outbox message {Id}", message.Id);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex,"Failed to publish outbox message {Id}",message.Id);
                    }

                    await dbContext.SaveChangesAsync(stoppingToken);
                   

                }
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
