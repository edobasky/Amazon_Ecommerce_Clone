using Catalog.Entities;
using Catalog.Responses;
using Catalog.Specifications;

namespace Catalog.Mappers
{
    public static class ProductMapper
    {
        public static ProductResponse ToResponse(this Product product)
        {
            if (product is null) return new ProductResponse();
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Summary = product.Summary,
                Description = product.Description,
                ImageFile = product.ImageFile,  
                Price = product.Price,
                Brand = product.Brand,
                Type    = product.Type,
            };
        }

        public static IEnumerable<ProductResponse> ToResponseList(this IEnumerable<Product> products) =>
            products.Select(p => p.ToResponse());
        public static Pagination<ProductResponse> ToResponse(this Pagination<Product> pagination)
        {
           return new Pagination<ProductResponse>(
                pagination.PageIndex,
                pagination.PageSize,
                pagination.Count,
                pagination.Data.Select(p => p.ToResponse()).ToList()
                );
        }
    }
}
