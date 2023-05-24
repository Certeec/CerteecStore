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

        public Cart? GetCartByUserId(Guid id)
        {
            ////Tenary conditional Operator
            bool result = InMemoryDatabase.Carts.TryGetValue(id, out Cart? currentCart);

            return result ? currentCart : null;
            
        }

        public void UpdateCart(Guid userId, Cart current)
        {
            InMemoryDatabase.Carts[userId] = current;
        }

        public bool CreateCart(Guid userId, Cart userCart)
        {
            InMemoryDatabase.Carts.Add(userId, userCart);
            return true; 
            // do ogarniecia
        }
    }
}
