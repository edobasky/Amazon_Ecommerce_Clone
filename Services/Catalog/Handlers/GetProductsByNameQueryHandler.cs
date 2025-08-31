using Catalog.Mappers;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers
{
    public class GetProductsByNameQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsByNameQuery, IList<ProductResponse>>
    {
        public async Task<IList<ProductResponse>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await productRepository.GetProductsByName(request.name); //

            return productList.ToResponseList().ToList();
        }
    }
}
