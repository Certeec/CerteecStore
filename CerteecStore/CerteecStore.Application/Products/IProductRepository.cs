
namespace CerteecStore.Application.Products
{
    public interface IProductRepository
    {
        List<Product> ReadAllProducts();

        int RemoveProductById(int id);

        Product? FindProductById(int productId);

        int AddProduct(Product productToAdd);

        List<Product> ReadProductsByArray(int[] productsIds);
    }
}
