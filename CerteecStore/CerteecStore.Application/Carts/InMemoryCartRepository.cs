using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Carts
{
    public class InMemoryCartRepository :  ICartRepository
    {

        public Cart FindCartByUserId(Guid id)
        {
            try
            {
                return InMemoryDatabase.Carts.First(n => n.Key == id).Value;
            }
            catch(Exception e)
            {
                // nie wiem czy to jest najrozsądniejsze rozwiązanie. Jako osoba wywołująca spodziewałbym się po nazwie metody, że jak nie znajdę koszyka to go nie dostanę
                // może jakbyś nazwał FindOrCreateCartByUserId?
                return new Cart();

            }
        }

        public Product FindProductById(int productId)
        {
            return InMemoryDatabase.Prodcuts.Single(n => n.ProductId == productId);
        }
        public void UpdateCartToDatabase(Guid userId, Cart current)
        {
            InMemoryDatabase.Carts[userId] = current;
        }
    }
}
