using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.DTOs;
using Ordering.Queries;

namespace Ordering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IMediator mediator, ILogger<OrderController> logger) : ControllerBase
    {
        [HttpGet("{username}", Name = "GetOrdersByUserName")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([FromRoute] string username)
        {
            var query = new GetOrderList(username);
            var orders = await mediator.Send(query);
            logger.LogInformation($"Orders fetched for user: {username}");
            return Ok(orders);
        }

        // testing purpose
        [HttpPost(Name = "CheckoutOrder")]
        public async async Task<ActionResult>([FromBody] CreateOrderDto dto)
        {
            var command = dto.ToCommand();
            var result = await mediator.Send(command);
            logger.LogInformation($"Order create with Id: {result.Id}");
            return Ok(result);
        }
    }
}
