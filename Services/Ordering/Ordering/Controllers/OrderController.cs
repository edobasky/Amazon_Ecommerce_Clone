using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Commands;
using Ordering.DTOs;
using Ordering.Mappers;
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
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CreateOrderDto dto)
        {
            var command = dto.ToCommand();
            var result = await mediator.Send(command);
            logger.LogInformation($"Order create with Id: {result}");
            return Ok(result);
        }

        [HttpPut(Name ="UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDto dto)
        {
            var command = dto.ToCommand();
            await mediator.Send(command);
            logger.LogInformation($"Order updated with Id: {dto.Id}");
            return NoContent();
        }

        [HttpDelete("{id}",Name = "DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            var command = new DeleteOrderCommand { Id = id };
            await mediator.Send(command);
            logger.LogInformation($"Order deleted with id: {id}");
            return NoContent();
        }
    }
}
