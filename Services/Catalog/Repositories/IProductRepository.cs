using Catalog.Entities;
using Catalog.Specifications;

namespace Catalog.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Pagination<Product>> GetProducts(CatalogSpecParams specParams);

        Task<IEnumerable<Product>> GetProductsByName(string name);
        public IEnumerable<Product> GetProductsByBrand(string name);
        Task<Product> CreateProduct(string productId);
        Task<Product> CreateProduct(Product product );
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);

        Task<productBrand> GetBrandByBrandIdAsync(string brandId);
        Task<ProductType> GetTypeByIdAsync(string typeId);
    }
}
