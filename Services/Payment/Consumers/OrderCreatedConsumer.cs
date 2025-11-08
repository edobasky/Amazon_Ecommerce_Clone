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

            // below is not coming as an http contect hence need to push serilog which logs as a context within the request scope
            using (Serilog.Context.LogContext.PushProperty("CorrelationId", message.CorrelationId))
            {
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
                    logger.LogInformation("Payment success for Order Id : {OrderId} and correlationId : {CorrelationId}", message.Id,message.CorrelationId);
                }
                else
                {
                    var failedEvent = new PaymentFailedEvent
                    {
                        OrderId = message.Id,
                        CorrelationId = context.CorrelationId ?? Guid.NewGuid(),
                        Reason = "Total price was zero or negative"
                    };
                    await publishEndpoint.Publish(failedEvent);
                    logger.LogWarning("Payment failed for order Id and correlationId --> {OrderId} : {CorrelationId}", message.Id, message.CorrelationId);
                }
            }
           
        }
    }
}
