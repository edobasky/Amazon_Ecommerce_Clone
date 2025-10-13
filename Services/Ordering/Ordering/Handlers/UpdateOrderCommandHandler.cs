using MediatR;
using Ordering.Commands;
using Ordering.Entities;
using Ordering.Exceptions;
using Ordering.Mappers;
using Ordering.Repositories;

namespace Ordering.Handlers
{
    public class UpdateOrderCommandHandler(IOrderRepository orderRepository,ILogger<UpdateOrderCommandHandler> logger) : IRequestHandler<UpdateOrderCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await orderRepository.GetByIdAsync(request.Id);
            if (orderToUpdate is null) throw new OrderNotFoundException(nameof(Order), request.Id);

            orderToUpdate.MapUpdate(request);
            await orderRepository.UpdateAsync(orderToUpdate);
            // Optional change: if status change needs to be known
            var outBoxMessage = OrderMapper.ToOutboxMessageForUpdate(orderToUpdate, request.CorrelationId);
            await orderRepository.AddOutboxMessageAsync(outBoxMessage);
           //*************
            logger.LogInformation($"Order {orderToUpdate.Id} is successfully Updated");
            return Unit.Value;
        }
    }
}
