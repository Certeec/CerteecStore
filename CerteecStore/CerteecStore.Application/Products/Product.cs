using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Products
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double ItemPrice { get; set; }
        public int Quantity { get; set; }

        /// Rozwazam usuniecie tu quantity.. albo zwracanie Produktu bez quantity..
        /// A moze.. klasa Product by dziedziczyla z klasy ProductToShow
        /// i ta klas product toShow by nie miala tego quantity dzieki czemu by sie latwo 
        /// zwracalo.. jest to jakas mysl
    }
}
