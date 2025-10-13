using EventBus.Messages.Events;
using MassTransit;
using Ordering.Entities;
using Ordering.Repositories;

namespace Ordering.EventBusConsumer
{
    public class PaymentCompletedConsumer(IOrderRepository orderRepository, ILogger<PaymentCompletedConsumer> logger) : IConsumer<PaymentCompletedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            var order = await orderRepository.GetByIdAsync(context.Message.OrderId);
            if (order is null)
            {
                logger.LogWarning("Order not found for Id: {OrderId} and {CorrelationId}", context.Message.OrderId, context.Message.CorrelationId);
                return;
            }

            order.Status = OrderStatus.Paid;
            await orderRepository.UpdateAsync(order);
            logger.LogInformation("Order Id {OrderId} marked as Paid", context.Message.OrderId);
        }
    }
}
