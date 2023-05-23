using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CerteecStore.Application.Database;

namespace CerteecStore.Application.Products
{
    public class InMemoryProductRepository : IProductRepository
    {
        public List<Product> ReadAll()
        {
            return InMemoryDatabase.Prodcuts;
        }

        public bool RemoveByProductId(int id)
        {
            InMemoryDatabase.Prodcuts.RemoveAll(n => n.ProductId == id);
            return true;
            ///Is not Crashing while item doesnt exist
            /// tu miales mi cos na ten temat podrzucic 
        }

        public Product FindProductById(int productId)
        {
            return InMemoryDatabase.Prodcuts.Single(n => n.ProductId == productId);
        }

        public bool AddProduct(Product productToAdd)
        {
            InMemoryDatabase.Prodcuts.Add(productToAdd);
            return true;
        }
    }
}
