using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CerteecStore.Application.Products;

namespace CerteecStore.Application.Carts
{
    public interface ICartRepository
    {
        Cart GetCartByUserId(Guid userId); /// Przneioslem Jedynie zwracam nulla
        void UpdateCart(Guid userId, Cart current);
        bool CreateCart(Guid userId, Cart cart);

        //void UpdateCart(Cart cart); /// czy napewno sam cart? dopisanie Guid ulatwia walidacje +
        /// pozwala na dodanie nowego carta w jednej funkcji

        //void/id/Cart CreateCart(Cart cart); - Zależy od Ciebie co chcesz zwracać, zwykle zwraca się ID utworzonego obiektu, ale możesz zwrócić cały
        /// a Jak moge zwrocic id utworzonego objektu gdy on nie ma id? 
        //List<Product> GetProductsFromCartByUserId(Guid userId); 
        /// czy potrzebujemy ta funckje? skoro wyciagamy caly koszyk poprzez fumckje getCart?
    }
}
