using Catalog.Commands;
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

        public static Product ToEntity(this CreateProductCommand command, ProductBrand brand, ProductType type) =>
            new Product
            {
                Name = command.Name,
                Summary = command.Summary,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Brand = brand,
                Type = type,
                Price = command.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

        public static Product ToUpdateEntity(this UpdateProductCommand command, Product existing, ProductBrand brand, ProductType type) =>
            new Product
            {
                Id = existing.Id,
                Name = command.Name,
                Summary = command.Summary,
                ImageFile = command?.ImageFile,
                Brand = brand,
                Type = type,
                Price = command.Price,
                CreatedDate = existing.CreatedDate, 
            };
    }
}
