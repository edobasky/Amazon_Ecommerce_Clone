using Basket.Commands;
using Basket.Mappers;
using Basket.Repositories.Interface;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers
{
    public class CreateShoppingCartCommandHandler(IBasketRepository basketRepository) : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            // convert command to domain entity
            var shoppingCartEntity = request.ToEntity();
            // save to Redis
            var updateCart = await basketRepository.UpsertBasket(shoppingCartEntity);

            // convert back to response
            return updateCart.ToResponse();

        }
    }
}
