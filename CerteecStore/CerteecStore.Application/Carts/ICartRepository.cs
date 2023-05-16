using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.Carts
{
    public interface ICartRepository
    {
        public Cart FindCartByUserId(Guid userId);
        public Product FindProductById(int productId);
    }
}
