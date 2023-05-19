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
            Cart userCart = FindOrCreateCartByUserId(userId);
            
            return value;
        }
    }
}
