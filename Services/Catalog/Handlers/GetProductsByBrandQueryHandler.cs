
using Catalog.Queries;
using Catalog.Mappers;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers
{
    public class GetProductsByBrandQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsByBrandQuery, IList<ProductResponse>>
    {
        public async Task<IList<ProductResponse>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
        {
            var productList = await productRepository.GetProductsByBrand(request.BrandName);
            return productList.ToResponseList().ToList();
        }
    }
}
