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
        Cart FindOrCreateCartByUserId(Guid userId); // według mnie tworzenie koszyka w repozytorium to strzał w kolano, bo to jest duży szczegół biznesowy, lepiej to robić w serwisie, a po prostu później dodać nowy do bazy. Czyli weź koszyk, jak nie ma to utwórz i zapisz.
        void UpdateCart(Guid userId, Cart current);
        double CountCartValue(Guid userId); // w tym przypadku chcesz zwrócić produkty, a policzyć je w serwisie
        Cart AddProductToCart(Guid userId, Product productToAdd, int quantity); // bardziej wolisz isć w kierunku Weź koszyk -> dodaj produkt -> zrób update koszyka
        int TakeProductFromTheCart(Guid userId, Product productToRemove); // to samo co prz dodawaniu produktu

        //Cart GetCartByUserId(Guid userId);

        //void UpdateCart(Cart cart);

        //void/id/Cart CreateCart(Cart cart); - Zależy od Ciebie co chcesz zwracać, zwykle zwraca się ID utworzonego obiektu, ale możesz zwrócić cały

        //List<Product> GetProductsFromCartByUserId(Guid userId);
    }
}
