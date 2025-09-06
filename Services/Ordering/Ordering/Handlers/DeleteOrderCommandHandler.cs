using MediatR;
using Ordering.Commands;
using Ordering.Entities;
using Ordering.Exceptions;
using Ordering.Repositories;

namespace Ordering.Handlers
{
    public class DeleteOrderCommandHandler(IOrderRepository orderRepository,ILogger<DeleteOrderCommandHandler> logger) : IRequestHandler<DeleteOrderCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await orderRepository.GetByIdAsync(request.Id);
            if (orderToDelete is null) throw new OrderNotFoundException(nameof(Order), request.Id);


            await orderRepository.DeleteAsync(orderToDelete);
            logger.LogInformation($"Order with Id {request.Id} have been deleted successfully");

            return Unit.Value;


        }
    }
}
