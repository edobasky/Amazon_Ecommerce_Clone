using MediatR;
using Ordering.Commands;
using Ordering.Mappers;
using Ordering.Repositories;

namespace Ordering.Handlers
{
    public class CheckOutOrderCommandHandler(IOrderRepository orderRepository, ILogger<CheckOutOrderCommandHandler> logger) : IRequestHandler<CheckOutOrderCommand, int>
    {
        public async Task<int> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = request.ToEntity();
            var generatedOrder = await orderRepository.AddAsync(orderEntity);
            var outboxMessage = OrderMapper.ToOutboxMessage(generatedOrder,request.CorrelationId);
            await orderRepository.AddOutboxMessageAsync(outboxMessage);
            logger.LogInformation($"Order with Id : {generatedOrder.Id} successfully created with outbox message and CorrelationId : {request.CorrelationId}");
            return generatedOrder.Id;
        }
    }
}
