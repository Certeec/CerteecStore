using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Carts
{
    public interface ICartService
    {
        Cart FindCartByUserId(Guid userId);
        void UpdateCart(Guid userId, Cart current);
        double CountCartValue(Guid userId);
        bool AddProductToCart(Guid userId, int idProductToAdd, int quantity);
        Cart CreateCart(Guid userId);
        int RemoveOneProductFromTheCart(Guid userId, int idProductToRemove);
        List<ProductInCartDTO> ShowAllProductsInCart(Guid userId);
    }
}
