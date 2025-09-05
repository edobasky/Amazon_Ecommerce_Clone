using Basket.Commands;
using Basket.GrpcService;
using Basket.Mappers;
using Basket.Repositories.Interface;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers
{
    public class CreateShoppingCartCommandHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService) : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            // Apply discount
            foreach (var item in request.Item)
            {
                var coupon = await discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= (decimal)coupon.Amount;
            }
            // convert command to domain entity
            var shoppingCartEntity = request.ToEntity();
            // save to Redis
            var updateCart = await basketRepository.UpsertBasket(shoppingCartEntity);

            // convert back to response
            return updateCart.ToResponse();

        }
    }
}
