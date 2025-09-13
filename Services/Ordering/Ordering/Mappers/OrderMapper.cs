using EventBus.Messages.Events;
using Ordering.Commands;
using Ordering.DTOs;
using Ordering.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
                AddressLine = command.AddressLine,
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

        public static CheckOutOrderCommand ToCommand(this CreateOrderDto dto)
        {
            return new CheckOutOrderCommand
            {
                UserName = dto.UserName,
                TotalPrice = dto.TotalPrice,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EmailAddress = dto.EmailAddress,
                AddressLine = dto.AddressLine,
                CardName = dto.CardName,
                CardNumber = dto.CardNumber,
                Expiration = dto.Expiration,
                Cvv = dto.Cvv,
                PaymentMethod = dto.PaymentMethod,
                Country = dto.Country,
                State = dto.State,
                ZipCode = dto.ZipCode,
            };
        }

        public static UpdateOrderCommand ToCommand(this OrderDto dto)
        {
            return new UpdateOrderCommand
            {
                Id = dto.Id,
                UserName = dto.UserName,
                TotalPrice = dto.TotalPrice,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                EmailAddress = dto.EmailAddress,
                AddressLine = dto.AddressLine,
                CardName = dto.CardName,
                CardNumber = dto.CardNumber,
                Expiration = dto.Expiration,
                Cvv = dto.Cvv,
                PaymentMethod = dto.PaymentMethod,
                Country = dto.Country,
                State = dto.State,
                ZipCode = dto.ZipCode,
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

        public static CheckOutOrderCommand ToCheckoutOrderCommand(this BasketCheckoutEvent message)
        {
            return new CheckOutOrderCommand
            {
                UserName = message.UserName!,
                TotalPrice = message.TotalPrice,
                FirstName = message.FirstName,
                LastName = message.LastName,
                AddressLine = message.AddressLine,
                EmailAddress = message.EmailAddress,
                Country = message.Country,
                State   = message.State,
                ZipCode = message.ZipCode,
                PaymentMethod = message.PaymentMethod,
                CardName = message.CardName,
                CardNumber  = message.CardNumber,
                Expiration = message.Expiration,    
                Cvv = message.Cvv,


            };
        }
    }
}
