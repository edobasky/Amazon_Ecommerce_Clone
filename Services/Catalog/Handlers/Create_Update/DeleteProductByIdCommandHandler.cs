using Catalog.Commands;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers.Create_Update
{
    public class DeleteProductByIdCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductByIdCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            return await productRepository.DeleteProduct(request.Id);
        }
    }
}
