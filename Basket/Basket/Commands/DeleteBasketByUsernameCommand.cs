using MediatR;

namespace Basket.Commands
{
    public record DeleteBasketByUsernameCommand(string userName) : IRequest<Unit>;
   
}
 