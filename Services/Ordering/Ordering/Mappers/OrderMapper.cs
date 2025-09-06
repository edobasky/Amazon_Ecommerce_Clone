using Ordering.Commands;
using Ordering.DTOs;
using Ordering.Entities;

namespace Ordering.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto(order.Id,order.UserName!,order.TotalPrice,order.FirstName!,
                                 order.LastName!,order.EmailAddress!,order.AddressLine!,
                                 order.Country!,order.State!,order.ZipCode!,order.CardName!,order.CardNumber!,
                                 order.Expiration!,order.Cvv!,order.PaymentMethod); 
        }

        public static Order ToEntity(this CheckOutOrderCommand command)
        {
            return new Order
            {
                UserName = command.UserName,
                TotalPrice = command.TotalPrice,
                FirstName = command.FirstName,
                LastName = command.LastName,
                EmailAddress = command.EmailAddress,
                AddressLine = command.AddreLine,
                CardName = command.CardName,
                CardNumber = command.CardNumber,
                Expiration = command.Expiration,
                Cvv = command.Cvv,
                PaymentMethod = command.PaymentMethod,
                Country = command.Country,
                State = command.State,
                ZipCode = command.ZipCode,

            };
        }

        public static void MapUpdate(this Order orderToUpdate, UpdateOrderCommand request)
        {
            orderToUpdate.UserName = request.UserName;
            orderToUpdate.TotalPrice = request.TotalPrice;
            orderToUpdate.FirstName = request.FirstName;
            orderToUpdate.LastName = request.LastName;
            orderToUpdate.CardNumber = request.CardNumber;
            orderToUpdate.Expiration = request.Expiration;
            orderToUpdate.CardName = request.CardName;
            orderToUpdate.AddressLine = request.AddressLine;
            orderToUpdate.Country = request.Country;
            orderToUpdate.State = request.State;
            orderToUpdate.Cvv = request.Cvv;
            orderToUpdate.PaymentMethod = request.PaymentMethod;
            orderToUpdate.EmailAddress  = request.EmailAddress;
            orderToUpdate.ZipCode = request.ZipCode;
        }
    }
}
