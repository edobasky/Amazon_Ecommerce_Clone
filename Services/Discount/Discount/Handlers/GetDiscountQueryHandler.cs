using Discount.DTOs;
using Discount.Mappers;
using Discount.Queries;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers
{
    public class GetDiscountQueryHandler(IDiscountRepository discountRepository) : IRequestHandler<GetDiscountQueries, CouponDto>
    {
        public async Task<CouponDto> Handle(GetDiscountQueries request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.productName))
            {
                var validationErrors = new Dictionary<string, string>
                {
                    {"ProductName","Product name must not be empty." }
                };
            } 
            var coupon = await discountRepository.GetDiscount(request.productName);
            if (coupon is null) throw new RpcException(new Status(StatusCode.NotFound, $"Discount for the Product Name = {request.productName} not found"));
            return coupon.ToDto();
        }

    }
}
