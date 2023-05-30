﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; // usingi do usunięcia

namespace CerteecStore.Application.Carts
{
    public class ProductInCartDTO
    {
        public double ProductId { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice => UnitPrice * Quantity;
    }
}
