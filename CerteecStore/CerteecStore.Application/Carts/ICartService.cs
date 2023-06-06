using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Carts
{
    public interface ICartService
    {
        decimal CountCartValue(int userId);
        int AddProductToCart(int userId, int idProductToAdd, int quantity);
        int RemoveOneProductFromTheCart(int userId, int idProductToRemove);
        List<ProductInCartDTO> ShowAllProductsInCart(int userId);
    }
}
