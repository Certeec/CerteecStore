using Castle.Core.Configuration;
using CerteecStore.Application.Carts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.UnitTests.Carts
{
    internal class CartRepositoryTests
    {
        CartRepository _cartRepositoryUT;
        Mock<IConfiguration> configuration;

        [SetUp]
        public void SetUp()
        {
            configuration = new Mock<IConfiguration>();
            _cartRepositoryUT = new CartRepository((Microsoft.Extensions.Configuration.IConfiguration)configuration.Object);
        }

        [Test]
        public void RemoveProductFromCart_Should()
        {
            _cartRepositoryUT.RemoveProductFromCart(1, 3);
        }
    }
}
