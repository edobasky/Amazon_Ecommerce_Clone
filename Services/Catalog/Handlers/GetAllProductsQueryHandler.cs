
using Catalog.Mappers;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Responses;
using Catalog.Specifications;
using MediatR;

namespace Catalog.Handlers
{
    public class GetAllProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
    {
        public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await productRepository.GetProducts(request.CatalogSpecParams);
            var productResponseList = productList.ToResponse();
            return productResponseList;
        }
    }
}
