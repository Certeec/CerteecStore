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
        private ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void AddProductToCart(Guid userId, int productId, int quantityToAdd)
        {
            Cart current =_cartRepository.FindCartByUserId(userId);
            Product productToAdd = _cartRepository.FindProductById(productId);

            if (current.Products.TryGetValue(productToAdd, out int currentAmount))
            {
                current.Products[productToAdd] = currentAmount + quantityToAdd;
            }
            else
            {
                current.Products.Add(productToAdd, quantityToAdd);
            }
            UpdateCartToDatabase(userId, current);
        }

        public void UpdateCartToDatabase(Guid userId,Cart current)
        {
            InMemoryDatabase.Carts[userId] = current;
        }

    }
}
