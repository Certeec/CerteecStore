using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CerteecStore.Application.Database;

namespace CerteecStore.Application.Products
{
    public class InMemoryProductRepository
    {
        private readonly InMemoryDatabase _memoryDatabase;
        
        public InMemoryProductRepository(InMemoryDatabase database)
        {
            _memoryDatabase = database;
        }

        public List<Product> ReadAllProducts()
        {
            return _memoryDatabase.Prodcuts; 
        }

        public int RemoveProductById(int id)
        {
            var result = _memoryDatabase.Prodcuts.RemoveAll(n => n.ProductId == id);
            return result; 
        }

        public Product FindProductById(int productId)
        {
            return _memoryDatabase.Prodcuts.SingleOrDefault(n => n.ProductId == productId);
        }

        public bool AddProduct(Product productToAdd)
        {
            _memoryDatabase.Prodcuts.Add(productToAdd);
            return true;
        }
    }
}
