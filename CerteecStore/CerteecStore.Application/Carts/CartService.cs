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
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productService)
        {
            _cartRepository = cartRepository;
            _productRepository = productService;
        }

        public decimal CountCartValue(int userId)
        {
            var productsToCount = ShowAllProductsInCart(userId);
            decimal valueOfCart = productsToCount.Sum(n => n.UnitPrice * n.Quantity); //Zastanawialem sie nad uzyciemTotalPrice ale risky

            return valueOfCart;
        }

        //Coalesc operator ??
        //public bool AddProductToCart(Guid userId, int idProductToAdd, int quantity)
        //{
        //    Product productToAdd = _productRepository.FindProductById(idProductToAdd);
        //    if (productToAdd == null)
        //    {
        //        return false;
        //    }

        //    Cart userCart = FindCartByUserId(userId) ?? CreateCart(userId);
        //    if(userCart.Products.ContainsKey(productToAdd))
        //    {
        //        userCart.Products[productToAdd] += quantity;
        //    }
        //    else
        //    {
        //        userCart.Products.Add(productToAdd, quantity);
        //    }

        //    UpdateCart(userId, userCart);
        //    return true;
        //}

        public int AddProductToCart(int userId, int productId, int quantity)
        {
            Product productToAdd = _productRepository.FindProductById(productId);
            if (productToAdd == null || productToAdd.Quantity < quantity)
            {
                return 0;
            }
         
            return _cartRepository.AddItemToCart(userId, productId, quantity);
        }


        public int RemoveOneProductFromTheCart(int userId, int productToRemoveId)
        {
            return _cartRepository.RemoveProductFromCart(userId, productToRemoveId);
        }


        //public int RemoveOneProductFromTheCart(Guid userId, int idProductToRemove)
        //{
        //    //Problems to solve -> if product is Null not gonna work... Also to Refactory.

        //    Product productToRemove = _productRepository.FindProductById(idProductToRemove);
        //    Cart userCart = FindCartByUserId(userId);

        //    if (userCart.Products.ContainsKey(productToRemove)) 
        //    {
        //        userCart.Products[productToRemove] -= 1;
        //        if (userCart.Products[productToRemove] < 1) // lepiej chyba wpisać == 0, wygląda trochę czytelniej
        //         ///Jak dasz ==0, to gdy bedziesz potem usuwac  - quantity i ci wyjdzie -5, to nie zadziala, a tak sie zabezpieczamy.
        //        {
        //            userCart.Products.Remove(productToRemove);
        //            UpdateCart(userId, userCart);
        //            return 0;
        //        }
        //        else
        //        {
        //            UpdateCart(userId, userCart);
        //            return userCart.Products[productToRemove];
        //        }

        //    }
        //    return -1;

        //}

        //public List<ProductInCartDTO> ShowAllProductsInCart(Guid userId)
        //{
        //    Cart userCart = FindCartByUserId(userId);
        //    if (userCart == null)
        //    {
        //        return new List<ProductInCartDTO>();
        //    }

        //    return userCart.Products.Select(n => new ProductInCartDTO
        //    {
        //        Name = n.Key.Name,
        //        ProductId = n.Key.ProductId,
        //        Quantity = n.Value,
        //        UnitPrice = n.Key.ItemPrice
        //    }).ToList();
        //}

        public List<ProductInCartDTO> ShowAllProductsInCart(int userId) // return ListOfProductInCartDTO
        {
            var Cart = _cartRepository.ShowAllProductsInCart(userId);
            int[] products = Cart.Select(n => n.ProductId).ToArray();
            List<Product> productsReturned = _productRepository.ReadProductsByArray(products);

            List<ProductInCartDTO> listToReturn = new List<ProductInCartDTO>();
            for(int i = 0;  i < productsReturned.Count(); i++)
            {
                ProductInCartDTO productInDto = new ProductInCartDTO()
                {
                    ProductId = productsReturned[i].ProductId,
                    Name = productsReturned[i].Name,
                    UnitPrice = productsReturned[i].ItemPrice,
                    Quantity = Cart[i].Quantity 
                };
                listToReturn.Add(productInDto);
            }
            return listToReturn;
        }
    }
}

