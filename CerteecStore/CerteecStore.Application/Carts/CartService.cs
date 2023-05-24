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
           Cart userCart = _cartRepository.GetCartByUserId(userId);
            if(userCart == null)
            {
                userCart = new Cart();
                _cartRepository.CreateCart(userId, userCart);
            }
            return userCart;
        }

        public void UpdateCart(Guid userId, Cart current)
        {
            _cartRepository.UpdateCart(userId, current);
        }

        public double CountCartValue(Guid userId)
        {
            double value = 0;
            Cart userCart = FindOrCreateCartByUserId(userId);
            for (int i = 0; i < userCart.Products.Count(); i++)
            {
                Product current = userCart.Products.ElementAt(i).Key;
                int multiplyBy = userCart.Products.ElementAt(i).Value;
                value += current.ItemPrice * multiplyBy;
            }

            return value;
        }

        public void AddProductToCart(Guid userId, int idProductToAdd, int quantity)
        {
            Product productToAdd = _productRepository.FindProductById(idProductToAdd);
            Cart userCart = FindOrCreateCartByUserId(userId);
            if(userCart.Products.ContainsKey(productToAdd))
            {
                userCart.Products[productToAdd] += quantity;
            }
            else
            {
                userCart.Products.Add(productToAdd, quantity);
            }

            UpdateCart(userId, userCart);
        }

        public int TakeProductFromTheCart(Guid userId, int idProductToRemove)
        {
            Product productToRemove = _productRepository.FindProductById(idProductToRemove);
            Cart userCart = FindOrCreateCartByUserId(userId);
            try
            {
                userCart.Products[productToRemove] -= 1;
                if (userCart.Products[productToRemove] < 1)
                {
                    userCart.Products.Remove(productToRemove);
                    UpdateCart(userId, userCart);
                    return 0;
                }
                else
                {
                    UpdateCart(userId, userCart);
                    return userCart.Products[productToRemove];
                }

            }
            catch(Exception e)
            {
                return -1;
            }
        }

        public List<ProductInCartDTO> ShowAllProductsInCart(Guid userId)
        {
            Cart userCart = _cartRepository.GetCartByUserId(userId);
            List<ProductInCartDTO> productsTransformed = new List<ProductInCartDTO>();
            foreach(var product in userCart.Products)
            {
                ProductInCartDTO productTransformed = new ProductInCartDTO()
                {
                    ProductId = product.Key.ProductId,
                    Name = product.Key.Name,
                    UnitPrice = product.Key.ItemPrice,
                    Quantity = product.Value
                };
                productsTransformed.Add(productTransformed);
            }
            return productsTransformed;

        }

    }
}

