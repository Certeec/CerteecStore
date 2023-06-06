using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CerteecStore.Application.Products; // nieużywane usingi
using CerteecStore.Application.Database;

namespace CerteecStore.Application.Carts
{
    public class InMemoryCartRepository
    {
        private readonly InMemoryDatabase _memoryDatbase;

        public InMemoryCartRepository(InMemoryDatabase memoryDatbase)
        {
            _memoryDatbase = memoryDatbase;
        }

        public Cart? GetCartByUserId(Guid id)
        {
            ////Tenary conditional Operator - do usunięcia
            bool result = _memoryDatbase.Carts.TryGetValue(id, out Cart? currentCart);

            return result ? currentCart : null;
        }

        public void UpdateCart(Guid userId, Cart current)
        {
            _memoryDatbase.Carts[userId] = current;
        }

        public void CreateCart(Guid userId, Cart userCart)
        {
            _memoryDatbase.Carts.Add(userId, userCart);
        }
    }
}
