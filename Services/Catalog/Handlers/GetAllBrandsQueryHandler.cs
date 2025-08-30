using Catalog.Extensions;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers
{
    public class GetAllBrandsQueryHandler(IBrandRepository brandRepository) : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
    {
        public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brandList = await brandRepository.GetAllBrands();
            return brandList.ToResponseList();
        }
    }
}
