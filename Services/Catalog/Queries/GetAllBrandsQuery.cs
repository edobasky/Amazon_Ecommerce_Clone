using Catalog.Responses;
using MediatR;

namespace Catalog.Queries
{
    public class GetAllBrandsQuery : IRequest<IList<BrandResponse>>
    {
    }
}
