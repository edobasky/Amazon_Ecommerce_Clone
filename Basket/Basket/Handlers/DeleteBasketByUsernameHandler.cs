using Basket.Commands;
using Basket.Repositories;
using Basket.Repositories.Interface;
using MediatR;

namespace Basket.Handlers
{
    public class DeleteBasketByUsernameHandler(IBasketRepository basketRepository) : IRequestHandler<DeleteBasketByUsernameCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteBasketByUsernameCommand request, CancellationToken cancellationToken)
        {
            await basketRepository.DeleteBasket(request.userName);
            return Unit.Value;
        }
    }
}
