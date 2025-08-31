using Catalog.Responses;
using MediatR;

namespace Catalog.Queries
{
    public record GetProductsByBrandQuery(string BrandName) : IRequest<IList<ProductResponse>>
    {
    }
}
