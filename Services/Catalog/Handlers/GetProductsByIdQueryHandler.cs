using Catalog.Mappers;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers
{
    public class GetProductsByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsByIdQuery, ProductResponse>
    {
        public async Task<ProductResponse> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetProduct(request.productId);
            return product.ToResponse();
        }
    }
}
