using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Mappers;

namespace Ordering.EventBusConsumer
{
    public class BasketOrderringConsumer(IMediator mediator, ILogger<BasketOrderringConsumer> logger) : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            using var scope = logger.BeginScope("Consuming Basket Checkout Event for {CorrelationId}", context.Message.CorrelationId);
            var command = context.Message.ToCheckoutOrderCommand();
            var result = await mediator.Send(command);
            logger.LogInformation("Basket Checkout Event completed successfully");
        }
    }
}
