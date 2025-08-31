using Catalog.Commands;
using Catalog.DTOs;
using Catalog.Mappers;
using Catalog.Queries;
using Catalog.Specifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IList<ProductDto>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetAllProductsQuery(catalogSpecParams);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(string id)
        {
            var query = new GetProductsByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("productName/{productName}")]
        public async Task<ActionResult<IList<ProductDto>>> GetProductByProductName(string productName)
        {
            var query = new GetProductsByNameQuery(productName);
            var result = await _mediator.Send(query);
            if (!result.Any()) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string Id)
        {
            var command = new DeleteProductByIdCommand(Id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string Id, UpdateProductDto updateProductDto)
        {
            var command = updateProductDto.ToCommand(Id);
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("GetAllBrands")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetBrands()
        {
            var query = new GetAllBrandsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetAllTypes")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetTypes()
        {
            var query = new GetAllTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("brand/{brand}", Name = "GetProductsByBrandName")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductByBrand(string brand)
        {
           return Ok(await _mediator.Send(new GetProductsByBrandQuery(brand)));
        }
    }
}