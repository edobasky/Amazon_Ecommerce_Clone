using Catalog.Commands;
using Catalog.DTOs;
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
                Type = product.Type,
                CreatedDate = product.CreatedDate,
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
        public static UpdateProductCommand ToCommand(this UpdateProductDto dto,string Id)
        {
            return new UpdateProductCommand
            {
                Id = Id,
                Name = dto.Name,
                Summary = dto.Summary,
                Description = dto.Description,
                ImageFile = dto.ImageFile,
                Price = dto.Price,
                BrandId = dto.BrandId,
                TypeId = dto.TypeId,
            };
        }
    }
}
