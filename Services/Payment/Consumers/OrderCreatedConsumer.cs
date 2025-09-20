using EventBus.Messages.Events;
using MassTransit;

namespace Payment.Consumers
{
    public class OrderCreatedConsumer(
            IPublishEndpoint publishEndpoint,
            ILogger<OrderCreatedConsumer> logger) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;
            logger.LogInformation("Processing payment for Order Id: {OrderId}", message.Id);

            //Simulate for Payment processing
            await Task.Delay(1000);
            if (message.TotalPrice > 0)
            {
                // Simulate Success
                var completedEvent = new PaymentCompletedEvent
                {
                    OrderId = message.Id,
                    CorrelationId = context.CorrelationId ?? Guid.NewGuid(),
                };
                await publishEndpoint.Publish(completedEvent);
                logger.LogInformation("Payment success for Order Id : {OrderId}", message.Id);
            }else
            {
                var failedEvent = new PaymentFailedEvent
                {
                    OrderId = message.Id,
                    CorrelationId = context?.CorrelationId ?? Guid.NewGuid(),
                    Reason = "Total price was zero or negative"
                };
                await publishEndpoint.Publish(failedEvent);
                logger.LogWarning("Payment failed for order Id : {OrderId}",message.Id);
            }
        }
    }
}
