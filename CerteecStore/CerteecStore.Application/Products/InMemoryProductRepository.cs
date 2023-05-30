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
        InMemoryDatabase _memoryDatabase; // private readonly
        
        public InMemoryProductRepository(InMemoryDatabase database)
        {
            _memoryDatabase = database;
        }

        public List<Product> ReadAll()
        {
            return _memoryDatabase.Prodcuts; 
        }

        public int RemoveProductById(int id)
        {
            var result = _memoryDatabase.Prodcuts.RemoveAll(n => n.ProductId == id);
            return result; 
            //Miales cos tutaj podrzucic.. zrobilem zeby dzialalo
            // ale jestem ciekaw co mialse na mysli

            /// nie pamiętam już, ale wygląda ok
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
