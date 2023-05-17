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
        private ICartRepository _cartRepository; // w przypadku takich "bezstanowych" serwisów, dobrą praktyką jest deklarować inne serwisy jako "readonly"
        private IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public void AddProductToCart(Guid userId, int productId, int quantityToAdd)
        {
            // np. nie chciałbyś żeby ktoś Ci zrobił np.:
            // _cartRepository = null;
            // a przez to, że to nie jest "readonly" to może

            Cart current = _cartRepository.FindCartByUserId(userId); 
            Product productToAdd = _productRepository.FindProductById(productId);

            // zastanawiam się czy by nie zadziałało gdybyś po prostu napisał:
            // current.Products[productToAdd] += quantityToAdd;
            // bez żadnych if-ów, ale to potrzebujesz testy żeby sobie to łatwo sprawdzić.
            if (current.Products.ContainsKey(productToAdd))
            {
                current.Products[productToAdd] += quantityToAdd;
            }
            else
            {
                current.Products.Add(productToAdd, quantityToAdd);
            }
          
            _cartRepository.UpdateCartToDatabase(userId, current);
        }
    }
}
