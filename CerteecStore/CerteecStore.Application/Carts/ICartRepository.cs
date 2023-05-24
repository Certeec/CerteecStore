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
        Cart? GetCartByUserId(Guid userId); /// Przneioslem Jedynie zwracam nulla
        void UpdateCart(Guid userId, Cart current);
        bool CreateCart(Guid userId, Cart cart); /// na Void 
    }
}
