using Discount.Commands;
using Discount.Extentions;
using Discount.Repositories;
using MediatR;

namespace Discount.Handlers
{
    public class DeleteDiscountCommandHandler(IDiscountRepository discountRepository) : IRequestHandler<DeleteDiscountCommand, bool>
    {
        public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.ProductName))
            {
                var validationErrors = new Dictionary<string, string> { { "ProductName", "Product name must not be empty." } };
                throw GrpcErrorHelper.CreateValidationException(validationErrors);
            }
            var deleted = await discountRepository.DeleteDiscount(request.ProductName);
            return deleted;
        }
    }
}
