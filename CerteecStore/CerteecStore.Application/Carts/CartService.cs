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
        private IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public void AddProductToCart(Guid userId, int productId, int quantityToAdd)
        {
            Cart current = _cartRepository.FindCartByUserId(userId); 
            Product productToAdd = _productRepository.FindProductById(productId);

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
