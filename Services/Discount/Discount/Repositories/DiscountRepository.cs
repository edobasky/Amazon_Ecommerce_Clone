using Dapper;
using Discount.Entities;
using Npgsql;

namespace Discount.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly string _connectionstring;

        public DiscountRepository(IConfiguration configuration)
        {
            _connectionstring = configuration.GetValue<string>("DatabaseSettings:Connectionstring") ?? throw new Exception("Connection string missing");
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_connectionstring);
            string query = @"INSERT INTO Coupon (ProductName,Description,Amount)
                             VALUES (@ProductName,@Description,@Amount)";
            var productToDiscount = await connection.ExecuteAsync(query, new { coupon.ProductName, coupon.Description, coupon.Amount });
                return productToDiscount > 0;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_connectionstring);
            string query = @"DELETE FROM Coupon WHERE ProductName = @ProductName";
            var result = await connection.ExecuteAsync(query,new {ProductName = productName});
            return result > 0;

        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_connectionstring);
            string query = "SELECT Id,ProductName,Description,Amount FROM Coupon WHERE ProductName = @ProductName";
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(query, new {ProductName = productName});
            return coupon ?? new Coupon
            {
                ProductName = "No Discount",
                Amount = 0,
                Description = "No Discount Available"
            };
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_connectionstring);
            string query = @"UPDATE Coupon SET ProductName = @ProductName,Description = @Description
                                Amount = @Amount WHERE Id = @Id";
            var result = await connection.ExecuteAsync(query, new {coupon.ProductName,coupon.Description,coupon.Amount});
            return result > 0;
        }
    }
}
