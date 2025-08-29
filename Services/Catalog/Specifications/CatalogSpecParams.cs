namespace Catalog.Specifications
{
    // filtering container that supports vatious filtering
    public class CatalogSpecParams
    {
        private const int MaxPageSize = 70;
        private int _pageSize = 10;
        public int pageIndes { get; set; } = 1;
        public int pageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? BrandId { get; set; }    
        public string? TypeId {  get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }
    }
}
