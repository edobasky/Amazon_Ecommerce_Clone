using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IBrandRepository
    {
        Task<IEnumerable<productBrand>> GetAllBrands();
        Task<productBrand> GetByIdAsync(string id);
    }
}
