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
        Cart? GetCartByUserId(Guid userId);
        void UpdateCart(Guid userId, Cart current);
        void CreateCart(Guid userId, Cart cart);
    }
}
