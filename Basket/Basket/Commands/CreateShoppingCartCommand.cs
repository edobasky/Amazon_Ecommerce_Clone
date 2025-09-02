﻿using Basket.DTOs;
using Basket.Responses;
using MediatR;

namespace Basket.Commands
{
    public record CreateShoppingCartCommand(string UserName, List<CreateShoppingCartItemDto> Item) : IRequest<ShoppingCartResponse>;
   
}
