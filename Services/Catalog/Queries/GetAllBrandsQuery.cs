using Catalog.Responses;
using MediatR;

namespace Catalog.Queries
{
    public record GetAllBrandsQuery(string BrandName) : IRequest<IList<BrandResponse>>
    {
    }
}
