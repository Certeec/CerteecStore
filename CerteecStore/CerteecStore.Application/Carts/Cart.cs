using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CerteecStore.Application.Products;

namespace CerteecStore.Application.Carts
{
    public class Cart
    {
        public Dictionary<Product, int> Products = new Dictionary<Product, int>();
    }
}
