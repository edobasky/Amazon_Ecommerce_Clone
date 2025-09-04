using Discount.DTOs;
using MediatR;

namespace Discount.Commands
{
    public record CreateDiscountCommand(string ProductName,string Description,decimal Amount) : IRequest<CouponDto>;
   
}
