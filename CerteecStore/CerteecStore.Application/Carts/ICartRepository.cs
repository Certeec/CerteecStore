
namespace CerteecStore.Application.Carts
{
    public interface ICartRepository
    {
        List<ProductsInCart> ShowAllProductsInCart(int userId);
        int RemoveProductFromCart(int userId, int productId);
        ProductsInCart GetProductQuantity(int userId, int productId);
        int InsertIntoCart(int userId, int productId, int quantity);
        int UpdateQuantityInCart(int userId, int productId, int quantity);

    }
}
