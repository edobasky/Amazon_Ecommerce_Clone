using Basket.Entities;

namespace Basket.Repositories.Interface
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpsertBasket(ShoppingCart shoppingCart);
        Task DeleteBasket(string userName);
    }
}
