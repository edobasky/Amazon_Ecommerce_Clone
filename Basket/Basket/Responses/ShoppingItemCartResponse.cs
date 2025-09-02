namespace Basket.Responses
{
    public record ShoppingItemCartResponse
    (
     string ProductId,
     string ProductName,
     int Quantity,
     decimal Price,
     string ImageFile
     );
}
