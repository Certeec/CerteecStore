using CerteecStore.Application.Database;
using CerteecStore.Application.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.UnitTests.Products
{
    internal class InMemoryProductRepositoryTests
    {
        InMemoryProductRepository _productRepositoryUT;
        [SetUp]
        public void SetUp()
        {
            _productRepositoryUT = new InMemoryProductRepository();
        }

        [Test]
        public void RemoveByProductId()
        {
            //Arrange

            //Act

            //Assert
        }



        [TearDown]
        public void TearDown()
        {
            InMemoryDatabase.Carts.Clear();
            InMemoryDatabase.Prodcuts.Clear();
        }
    }
}
