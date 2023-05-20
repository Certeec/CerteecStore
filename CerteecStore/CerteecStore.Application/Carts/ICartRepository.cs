using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CerteecStore.Application.Products;

namespace CerteecStore.Application.Carts
{
    public interface ICartRepository
    {
        Cart FindOrCreateCartByUserId(Guid userId);
        void UpdateCart(Guid userId, Cart current);
        double CountCartValue(Guid userId);
        void AddProductToCart(Guid userId, Product productToAdd, int quantity);
    }
}
