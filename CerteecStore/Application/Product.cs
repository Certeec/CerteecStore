using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double ItemPrice { get; set; }
        public int Quantity { get; set; }

        public Product(int productId, string name, string description, double itemPrice, int quantity)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            ItemPrice = itemPrice;
            Quantity = quantity;
        }
    }
}
