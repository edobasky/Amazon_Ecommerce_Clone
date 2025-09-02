using Basket.Mappers;
using Basket.Queries;
using Basket.Repositories.Interface;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers
{
    public class GetBasketByUserNameQueryHandler(IBasketRepository basketRepository) : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await basketRepository.GetBasket(request.UserName);
            if (shoppingCart is null) return new ShoppingCartResponse(request.UserName) { Items = new()};
            return shoppingCart.ToResponse();
        }
    }
}
