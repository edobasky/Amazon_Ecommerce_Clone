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
    public class UpdateDiscountCommandHandler(IDiscountRepository discountRepository) : IRequestHandler<UpdateDiscountCommand, CouponDto>
    {
        public async Task<CouponDto> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            // Input Validation
            Dictionary<string, string> validationErrors = UtilManager.ErrorValidationCheck(request);

            if (validationErrors.Any()) throw GrpcErrorHelper.CreateValidationException(validationErrors);

            var coupon = request.ToEntity();

            var updated = await discountRepository.UpdateDiscount(coupon);
            if (!updated) throw new RpcException(new Status(StatusCode.NotFound, $"Discount update failed for Product = {request.ProductName}"));

            return coupon.ToDto();
        }
    }

}
