using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Products
{
    public interface IProductService
    {
        List<Product> ReadAll();
        int RemoveProductById(int id);
        Product FindProductById(int productId);
        bool AddProduct(Product productToAdd);
    }
}
