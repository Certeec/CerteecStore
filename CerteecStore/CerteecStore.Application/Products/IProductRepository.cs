using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Products
{
    public interface IProductRepository
    {
        public List<Product> ReadAll();

        public bool RemoveByProductId(int id);

        public Product FindProductById(int productId);

        public bool AddProduct(Product productToAdd);

    }
}
