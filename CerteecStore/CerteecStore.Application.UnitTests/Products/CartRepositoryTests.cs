using CerteecStore.Application.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.UnitTests.Products
{
    internal class CartRepositoryTests
    {
        CartRepository _cartRepositoryUT;

        [SetUp]
        public void SetUp()
        {
            _cartRepositoryUT = new CartRepository();
        }

        [Test]
        public void RemoveProductFromCart_Should()
        {
            _cartRepositoryUT.RemoveProductFromCart(1, 3);
        }
    }
}
