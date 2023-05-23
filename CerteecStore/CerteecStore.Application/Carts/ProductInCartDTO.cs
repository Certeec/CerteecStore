using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Carts
{
    public class ProductInCartDTO
    {
        public double ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        //public ProductInCart(double productId, string name, double price, int quantity)
        //{
        //    ProductId = productId;
        //    Name = name;
        //    Price = price;
        //    Quantity = quantity;
        //}
    }
}
