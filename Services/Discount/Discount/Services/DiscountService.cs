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
    }
}
