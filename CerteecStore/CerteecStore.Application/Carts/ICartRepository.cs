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
        Product FindProductById(int productId); // ta metoda jest chyba do usunięcia? To samo masz w IProductRepository
        void UpdateCart(Guid userId, Cart current);
    }
}
