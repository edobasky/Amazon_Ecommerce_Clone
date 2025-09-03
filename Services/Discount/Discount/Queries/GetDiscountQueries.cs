using Discount.DTOs;
using MediatR;

namespace Discount.Queries
{
    public record GetDiscountQueries(string productName) : IRequest<CouponDto>;
 
}
