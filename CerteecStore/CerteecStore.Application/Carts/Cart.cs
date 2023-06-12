
using CerteecStore.Application.Products;

namespace CerteecStore.Application.Carts
{
    public class Cart
    {
        public Dictionary<Product, int> Products = new Dictionary<Product, int>();
    }
}
