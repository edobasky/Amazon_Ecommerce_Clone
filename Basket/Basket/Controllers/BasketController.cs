using Basket.Commands;
using Basket.DTOs;
using Basket.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{username}")]
        public async Task<ActionResult<ShoppingCartDto>> GetBasket(string username)
        {
            var query = new GetBasketByUserNameQuery(username);
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCartDto>> CreateOrUpdateBasket([FromBody] CreateShoppingCartCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            await mediator.Send(new DeleteBasketByUsernameCommand(userName));
            return Ok();
        }
    }
}
