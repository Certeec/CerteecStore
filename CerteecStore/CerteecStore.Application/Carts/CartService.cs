using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Carts
{
    public class CartService
    {
        private ICartRepository _cartRepository; // odstęp pomiędzy konstruktorem i właściwością
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void AddProductToCart(Guid userId, int productId, int quantityToAdd)
        {
            Cart current =_cartRepository.FindCartByUserId(userId); // spacja po =
            Product productToAdd = _cartRepository.FindProductById(productId); // według mnie metoda FindProductById niezbyt pasuje do ICartRepository?

            // ten if mógłbyś napisać jako:
            //if(current.Products.ContainsKey(productToAdd))
            //{
            //    current.Products[productToAdd] += quantityToAdd;
            //}
            //Wydaje mi się, że wygląda trochę czyściej
            if (current.Products.TryGetValue(productToAdd, out int currentAmount))
            {
                current.Products[productToAdd] = currentAmount + quantityToAdd;
            }
            else
            {
                current.Products.Add(productToAdd, quantityToAdd);
            } // pusta linia
            UpdateCartToDatabase(userId, current);
        }

        // Super sobie poradziłeś, z wymyśleniem ICartRepository, ale zobacz co tu się stało,
        // nasz CartService zaczął zależeć od InMemoryDatabase, a co jeśli będziemy chcieli zmienić bazę?
        public void UpdateCartToDatabase(Guid userId,Cart current)
        {
            InMemoryDatabase.Carts[userId] = current;
        }

    }
}
