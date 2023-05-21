using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CerteecStore.Application.Products;
using CerteecStore.Application.Database;

namespace CerteecStore.Application.Carts
{
    public class InMemoryCartRepository :  ICartRepository
    {

        public Cart FindOrCreateCartByUserId(Guid id)
        {
            try
            {
                return InMemoryDatabase.Carts.First(n => n.Key == id).Value;
            }
            catch(Exception e)
            {
                return new Cart();
            }
        }

        public void UpdateCart(Guid userId, Cart current)
        {
            InMemoryDatabase.Carts[userId] = current;
        }

        public double CountCartValue(Guid userId)
        {
            double value = 0;
            Cart userCart = FindOrCreateCartByUserId(userId);
            for(int i = 0; i < userCart.Products.Count(); i++)
            {
                Product current = userCart.Products.ElementAt(1).Key;
                int multiplyBy = userCart.Products.ElementAt(1).Value;
                value += current.ItemPrice * multiplyBy; 
            }
            
            return value;
        }

        public void AddProductToCart(Guid userId, Product productToAdd, int quantity)
        {
            Cart userCart = FindOrCreateCartByUserId(userId);
            userCart.Products.Add(productToAdd, quantity);
            
        }

        public int TakeProductFromTheCart(Guid userId, Product productToRemove)
        {

            //This function needs to be Fixed. Throwing expection at line 60.
            // problem occuring while removing Value by key;
            Cart userCart = FindOrCreateCartByUserId(userId);
            try
            {
                userCart.Products[productToRemove] -= 1;
                if (userCart.Products[productToRemove] < 1)
                {
                    userCart.Products.Remove(productToRemove);
                }
                return userCart.Products[productToRemove];
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
          
        }
    }
}
