using CerteecStore.Application.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application
{
    public interface IProductRepository
    {
        public List<Product> ReadAll();

        public bool RemoveByProductId(int id);

        public Product FindProductById(int productId);

    }
}
