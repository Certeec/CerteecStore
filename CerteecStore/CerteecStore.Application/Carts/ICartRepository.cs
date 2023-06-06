using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CerteecStore.Application.Products; // Nieużywane usingi

namespace CerteecStore.Application.Carts
{
    public interface ICartRepository
    {
        //Cart? GetCartByUserId(Guid userId);
        //void UpdateCart(Guid userId, Cart current);
        //void CreateCart(Guid userId, Cart cart);
        int AddItemToCart(int userId, int productId, int quantity);
        List<ProductsInCart> ShowAllProductsInCart(int userId);
        int RemoveProductFromCart(int userId, int productId);

    }
}
