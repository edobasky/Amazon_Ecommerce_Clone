using Basket.Commands;
using Basket.DTOs;
using Basket.Mappers;
using Basket.Queries;
using MassTransit;
using MediatR;

namespace Basket.Handlers
{
    public class CheckoutBasketCommandHandler(IMediator mediator,IPublishEndpoint publishEndpoint, ILogger<CheckoutBasketCommandHandler> logger) : IRequestHandler<CheckoutBasketCommand, Unit>
    {
        public async Task<Unit> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var basketResponse = await mediator.Send(new GetBasketByUserNameQuery(dto.UserName),cancellationToken);
            if (basketResponse is null || !basketResponse.Items.Any()) throw new InvalidOperationException("Basket not found or empty");
            var basket = basketResponse.ToEntity();

            //Map
            var evt = dto.ToBasketCheckoutEvent(basket);
            logger.LogInformation("Publishing BasketCheckoutEvent for {User}",basket.UserName);
            await publishEndpoint.Publish(evt,cancellationToken);

            // delete the basket
            await mediator.Send(new DeleteBasketByUsernameCommand(dto.UserName),cancellationToken);

            return Unit.Value;

        }
    }
}
