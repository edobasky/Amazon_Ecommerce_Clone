using Discount.Commands;
using Discount.Grpc.Protos;
using Discount.Mappers;
using Discount.Queries;
using Grpc.Core;
using MediatR;

namespace Discount.Services
{
    public class DiscountService(IMediator mediator) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetDiscountQueries(request.ProductName);
            var dto = await mediator.Send(query);
            return dto.ToModel();
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var cmd = request.Coupon.ToCreateCommand();
            var dto = await mediator.Send(cmd);
            return dto.ToModel();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var cmd = request.Coupon.ToUpdateCommand();
            var dto = await mediator.Send(cmd);
            return dto.ToModel();

        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var cmd = new DeleteDiscountCommand(request.ProductName);
            var deleted = await mediator.Send(cmd);
            return new DeleteDiscountResponse
            {
                Success = deleted,
            };
        }
    }
}
