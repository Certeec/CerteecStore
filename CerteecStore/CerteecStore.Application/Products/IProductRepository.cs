using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Products
{
    public interface IProductRepository
    {
        List<Product> ReadAllProducts();

        int RemoveProductById(int id);

        Product FindProductById(int productId);

        int AddProduct(Product productToAdd);

    }
}
