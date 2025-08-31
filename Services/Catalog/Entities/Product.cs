
namespace Catalog.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; init; }
        public string? Summary { get; init; }
        public string? Description { get; init; }
        public string? ImageFile { get; init; }
        public ProductBrand? Brand { get; init; }
        public ProductType? Type { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
