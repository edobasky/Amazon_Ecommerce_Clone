using Discount.DTOs;
using MediatR;

namespace Discount.Commands
{
    public record UpdateDiscountCommand(int Id,string ProductName,string Description,decimal Amount) : IRequest<CouponDto>;
    
}
