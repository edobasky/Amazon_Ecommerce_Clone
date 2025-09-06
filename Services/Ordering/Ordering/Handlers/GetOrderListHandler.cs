using MediatR;
using Ordering.DTOs;
using Ordering.Mappers;
using Ordering.Queries;
using Ordering.Repositories;

namespace Ordering.Handlers
{
    public class GetOrderListHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderList, List<OrderDto>>
    {
        public async Task<List<OrderDto>> Handle(GetOrderList request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrdersByUserName(request.UserName);
            return orders.Select(o => o.ToDto()).ToList();
        }
    }
}
