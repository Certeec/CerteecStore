using CerteecStore.Application.Products;
using static CerteecStore.Application.Carts.CartService;

namespace CerteecStore.Application.Carts
{
    public interface ICartService
    {
        decimal CountCartValue(int userId);
        int AddProductToCart(int userId, int idProductToAdd, int quantity);
        int RemoveProductFromTheCart(int userId, int idProductToRemove);
        Dictionary<Product, int> ShowAllProductsInCart(int userId);
        void UpdateProductQuantityInCart(int userId, int productId, QuantityCalc action);
    }
}
