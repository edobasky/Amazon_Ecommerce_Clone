using Basket.Commands;
using Basket.DTOs;
using Basket.Entities;
using Basket.Responses;
using EventBus.Messages.Events;

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

        public static ShoppingCart ToEntity(this ShoppingCartResponse response)
        {
            return new ShoppingCart(response.UserName)
            {
                Items = response.Items.Select(item => new ShoppingCartItems
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            };
        }

        public static BasketCheckoutEvent ToBasketCheckoutEvent(this BasketCheckoutDto dto, ShoppingCart basket)
        {
            return new BasketCheckoutEvent
            {
                UserName = dto.UserName,
                TotalPrice = basket.Items.Sum(item => item.Price * item.Quantity),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EmailAddress = dto.EmailAddress,
                AddressLine = dto.AddressLine,
                Country = dto.Country,
                State = dto.State,
                ZipCode = dto.ZipCode,
                CardName = dto.CardName,
                CardNumber = dto.CardNumber,
                Expiration = dto.Expiration,
                Cvv = dto.Cvv,  
                PaymentMethod = dto.PaymentMethod
            };
        }
    }
}
 