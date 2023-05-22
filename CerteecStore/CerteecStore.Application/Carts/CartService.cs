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

        public Cart FindOrCreateCartByUserId(Guid userId)
        {
           return _cartRepository.FindOrCreateCartByUserId(userId);
           
        }

        public void UpdateCart(Guid userId, Cart current)
        {
            _cartRepository.UpdateCart(userId, current);
        }

        public double CountCartValue(Guid userId)
        {
             return _cartRepository.CountCartValue(userId);
        }

        public void AddProductToCart(Guid userId, Product productToAdd, int quantity)
        {
            Cart userCart = _cartRepository.AddProductToCart(userId, productToAdd, quantity);
            UpdateCart(userId, userCart);
        }

        public int TakeProductFromTheCart(Guid userId, int idProductToRemove)
        {
            Product productToRemove = _productRepository.FindProductById(idProductToRemove);
            return _cartRepository.TakeProductFromTheCart(userId, productToRemove);
        }

        public Dictionary<Product, int> ShowAllProductsInCart(Guid userId)
        {
            Cart userCart = _cartRepository.FindOrCreateCartByUserId(userId);
            return userCart.Products;

            /// Ta funckja na razie nie dziala ( i wszystkie pochodne czyli wolanie itp)
            /// dlatego ze nie moge zwracac dictionary w Api wiec musze to inaczej wymyslic.
        }


        //public void AddProductToCart(Guid userId, int productId, int quantityToAdd)
        //{
        //    Cart current = _cartRepository.FindOrCreateCartByUserId(userId);
        //    Product productToAdd = _productRepository.FindProductById(productId);

        //    // zastanawiam się czy by nie zadziałało gdybyś po prostu napisał:
        //    // current.Products[productToAdd] += quantityToAdd;
        //    // bez żadnych if-ów, ale to potrzebujesz testy żeby sobie to łatwo sprawdzić.
        //    if (current.Products.ContainsKey(productToAdd))
        //    {
        //        current.Products[productToAdd] += quantityToAdd;
        //    }
        //    else
        //    {
        //        current.Products.Add(productToAdd, quantityToAdd);
        //    }

        //    _cartRepository.UpdateCart(userId, current);
        //}
    }
}

