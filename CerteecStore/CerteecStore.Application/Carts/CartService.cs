using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CerteecStore.Application.Products;

namespace CerteecStore.Application.Carts
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository; 
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public void AddProductToCart(Guid userId, int productId, int quantityToAdd)
        {
            Cart current = _cartRepository.FindOrCreateCartByUserId(userId); 
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
          
            _cartRepository.UpdateCart(userId, current);
        }
    }
}

