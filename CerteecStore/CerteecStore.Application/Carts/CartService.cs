using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CerteecStore.Application.Products;

namespace CerteecStore.Application.Carts
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productService)
        {
            _cartRepository = cartRepository;
            _productRepository = productService;
        }

        public decimal CountCartValue(int userId)
        {
            var productsToCount = ShowAllProductsInCart(userId);
            decimal valueOfCart = productsToCount.Sum(n => n.Key.ItemPrice * n.Value);

            return valueOfCart;
        }

        public delegate int QuantityCalc(int x);
        //Coalesc operator ??

        public void UpdateProductQuantityInCart(int userId, int productId, QuantityCalc action)
        {

            var result = _cartRepository.GetProductQuantity(userId, productId);
            var quantityToAdd = action(result.Quantity);
            _cartRepository.UpdateQuantityInCart(userId, productId, quantityToAdd);

        }

        public int AddProductToCart(int userId, int productId, int quantity)
        {
            Product productToAdd = _productRepository.FindProductById(productId);
            if (productToAdd == null || productToAdd.Quantity < quantity)
            {
                return 0;
            }

            var result = _cartRepository.GetProductQuantity(userId, productId);



            return result == null
                ? _cartRepository.InsertIntoCart(userId, productId, quantity)
                : _cartRepository.UpdateQuantityInCart(userId, productId, quantity + result.Quantity);
        }

        public int RemoveProductFromTheCart(int userId, int productToRemoveId)
        {
            return _cartRepository.RemoveProductFromCart(userId, productToRemoveId);
        }

        public Dictionary<Product,int> ShowAllProductsInCart(int userId) 
        {
            var Cart = _cartRepository.ShowAllProductsInCart(userId);
            int[] products = Cart.Select(n => n.ProductId).ToArray();
            List<Product> productsReturned = _productRepository.ReadProductsByArray(products);
            Dictionary<Product, int> productsToReturn = new Dictionary<Product, int>();

            for(int i =0; i < productsReturned.Count(); i++)
            {
                productsToReturn.Add(productsReturned[i], Cart[i].Quantity);
            }

            return productsToReturn;

        }
    }
}

