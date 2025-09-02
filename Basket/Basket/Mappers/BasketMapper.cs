using Basket.Commands;
using Basket.Entities;
using Basket.Responses;

namespace Basket.Mappers
{
    public static class BasketMapper
    {
        public static ShoppingCartResponse ToResponse(this ShoppingCart shoppingCart)
        {
            return new ShoppingCartResponse(
                 shoppingCart.UserName,
                 shoppingCart.Items.Select(item => new ShoppingItemCartResponse
                     (
                         item.ProductId,
                         item.ProductName,
                         item.Quantity,
                         item.Price,
                         item.ImageFile
                      )).ToList()
               );
        }

        public static ShoppingCart ToEntity(this CreateShoppingCartCommand command)
        {
            return new ShoppingCart
            {
                UserName = command.UserName,
                Items = command.Item.Select(item => new ShoppingCartItems
                {
                    ProductId = item.ProductId,
                      ProductName = item.ProductName,
                        Quantity = item.Quantity,
                       Price =  item.Price,
                       ImageFile =  item.ImageFile
                }).ToList()
            };
        }
    }
}
 