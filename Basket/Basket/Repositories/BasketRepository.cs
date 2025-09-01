using System.Text.Json;
using Basket.Entities;
using Basket.Repositories.Interface;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Repositories
{
    public class BasketRepository(IDistributedCache redisCache) : IBasketRepository
    {
        public async Task DeleteBasket(string userName)
        {
            await redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket)) return null;
            return JsonSerializer.Deserialize<ShoppingCart>(basket)!;
        }

        // Upsert operation
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            await redisCache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart));
            return await GetBasket(shoppingCart.UserName);
        }
    }
}
