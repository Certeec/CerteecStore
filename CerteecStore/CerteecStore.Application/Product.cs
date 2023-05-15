using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double ItemPrice { get; set; }
        public int Quantity { get; set; }

        // nie musisz deklarować konstruktora. Zauważ, że wszystkie Twoje właściwości są publiczne i równie dobrze możesz pisać:
        //Product product = new Product
        //{
        //    ProductId = ...
        //    Name = ...
        //    ...
        //}
        //Na pewno bardzo Ci to ułatwi przy testach, bo w zależności od potrzeb nie będziesz musiał wypełniać całego obiektu.

        /// zmienione
      
    }
}
