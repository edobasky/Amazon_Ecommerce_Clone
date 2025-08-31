using Catalog.Commands;
using Catalog.Mappers;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers.Create_Update
{
    public class UpdateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand, bool>
    {
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existing = await productRepository.GetProduct(request.Id);
            if (existing is null) throw new KeyNotFoundException($"Product with Id {request.Id} not found");

            // step 1 : Fetch Brand and Type
            var brand = await productRepository.GetBrandByIdAsync(request.Id);
            var type = await productRepository.GetTypeByIdAsync(request.Id);

            if (brand is null || type is null) throw new ApplicationException("Invalid Brand or Type specified");

            // Step 2: Mapper role
            var updateProduct = request.ToUpdateEntity(existing, brand, type);

            // step 3 : Save the record
            return await productRepository.UpdateProduct(updateProduct);
        }
    }
}
