namespace Basket.Responses
{
    public record class ShoppingCartResponse(string UserName,List<ShoppingItemCartResponse> Items)
    {
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

        public ShoppingCartResponse(string userName) : this(userName, new List<ShoppingItemCartResponse>())
        {

        }
    }
 
}