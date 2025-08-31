using Catalog.Commands;
using Catalog.Mappers;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers.Create_Update
{
    public class CreateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // fetch brand and Type from repo
            var brand = await productRepository.GetBrandByIdAsync(request.BrandId);
            var type = await productRepository.GetTypeByIdAsync(request.TypeId);

            if (brand is null || type is null) throw new ApplicationException("Invalid Brand or Type specified");

            // Match to entity
            var productEntity = request.ToEntity(brand, type);
            var newProduct = await productRepository.CreateProduct(productEntity);
            return newProduct.ToResponse();


        }
    }
}
