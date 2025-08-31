using Catalog.Responses;
using MediatR;

namespace Catalog.Queries
{
    public record GetProductsByIdQuery(string productId) : IRequest<ProductResponse>
    {
    }
}
