using Discount.Commands;
using Discount.DTOs;
using Discount.Extentions;
using Discount.Mappers;
using Discount.Repositories;
using Discount.Utility;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers
{
    public class CreateDiscountCommandHandler(IDiscountRepository discountRepository) : IRequestHandler<CreateDiscountCommand, CouponDto>
    {
        public async Task<CouponDto> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            // Input Validation
            Dictionary<string, string> validationErrors = UtilManager.ErrorValidationCheck(request);

            if (validationErrors.Any()) throw GrpcErrorHelper.CreateValidationException(validationErrors);

            var coupon = request.ToEntity();

            var created = await discountRepository.CreateDiscount(coupon);
            if (!created) throw new RpcException(new Status(StatusCode.Internal, $"Could not create discount for product: {request.ProductName}"));


               return coupon.ToDto();
        }

       
    }
}
