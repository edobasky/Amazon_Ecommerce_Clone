using EventBus.Messages.Events;
using MassTransit;
using Ordering.Repositories;

namespace Ordering.EventBusConsumer
{
    public class PaymentFailedConsumer(IOrderRepository orderRepository,ILogger<PaymentFailedEvent> logger) : IConsumer<PaymentFailedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            var order = await orderRepository.GetByIdAsync(context.Message.OrderId);
            if (order is null)
            {
                logger.LogWarning("Order not found for id : {OrderId} and {CorrelationId}", context.Message.OrderId,context.Message.CorrelationId);
                return;
            }
            order.Status = Entities.OrderStatus.Failed;
            await orderRepository.UpdateAsync(order);
            logger.LogInformation("Payment failed for Order Id: {OrderId}, Reason: {Reason}", context.Message.OrderId,context.Message.Reason);
        }
    }
}
