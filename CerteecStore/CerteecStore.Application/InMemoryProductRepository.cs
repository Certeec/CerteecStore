using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application
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

        }
    }
}
