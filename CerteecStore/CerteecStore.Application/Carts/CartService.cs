using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CerteecStore.Application.Products;

namespace CerteecStore.Application.Carts
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository; 
        private readonly IProductService _productService;

        public CartService(ICartRepository cartRepository, IProductService productService)
        {
            _cartRepository = cartRepository;
            _productService = productService;
        }
        
        public Cart FindCartByUserId(Guid userId)
        {
           Cart userCart = _cartRepository.GetCartByUserId(userId);

           return userCart; // możesz od razu napisać return _cartRepository.GetCartByUserId(userId);
        }

        // metoda może być prywatna, nie używasz jej nigdzie po za CartService
        // Druga sprawa czy ona jest potrzeba? Możesz we wszystkich miejscach używać wprost _cartRepository.UpdateCart(...)
        public void UpdateCart(Guid userId, Cart current)
        {
            _cartRepository.UpdateCart(userId, current);
        }

        public double CountCartValue(Guid userId)
        {
            Cart userCart = FindCartByUserId(userId);

            return userCart.Products.Sum(n => n.Key.ItemPrice * n.Value);
        }

        //Coalesc operator ??
        public bool AddProductToCart(Guid userId, int idProductToAdd, int quantity)
        {
            Product productToAdd = _productService.FindProductById(idProductToAdd);
            if (productToAdd == null)
            {
                return false;
            }

            Cart userCart = FindCartByUserId(userId) ?? CreateCart(userId);
            if(userCart.Products.ContainsKey(productToAdd))
            {
                userCart.Products[productToAdd] += quantity;
            }
            else
            {
                userCart.Products.Add(productToAdd, quantity);
            }

            UpdateCart(userId, userCart);
            return true;
        }

        public Cart CreateCart(Guid userId)
        {
            Cart userCart = new Cart();
            _cartRepository.CreateCart(userId, userCart);

            return userCart;
        }

        public int RemoveOneProductFromTheCart(Guid userId, int idProductToRemove)
        {
            Product productToRemove = _productService.FindProductById(idProductToRemove);
            Cart userCart = FindCartByUserId(userId);
            try
            {
                userCart.Products[productToRemove] -= 1;
                if (userCart.Products[productToRemove] < 1) // lepiej chyba wpisać == 0, wygląda trochę czytelniej
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
                return -1; // Po co Ci ten wyjątek tutaj?
            }
        }

        public List<ProductInCartDTO> ShowAllProductsInCart(Guid userId)
        {
            Cart userCart = FindCartByUserId(userId);
            if (userCart == null)
            {
                return new List<ProductInCartDTO>();
            }

            return userCart.Products.Select(n => new ProductInCartDTO
            {
                Name = n.Key.Name,
                ProductId = n.Key.ProductId,
                Quantity = n.Value,
                UnitPrice = n.Key.ItemPrice
            }).ToList<ProductInCartDTO>();
        }
    }
}

