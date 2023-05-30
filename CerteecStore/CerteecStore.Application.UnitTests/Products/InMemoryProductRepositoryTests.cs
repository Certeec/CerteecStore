using CerteecStore.Application.Database;
using CerteecStore.Application.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.UnitTests.Products
{
    /// TE TESTY WYDAJĄ SIĘ CHYBA JESZCZE NIE SKOŃCZONE
    internal class InMemoryProductRepositoryTests
    {
        InMemoryProductRepository _productRepositoryUT;
        InMemoryDatabase _database = new InMemoryDatabase();
        [SetUp]
        public void SetUp()
        {
            _productRepositoryUT = new InMemoryProductRepository(_database);
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
            _database.Carts.Clear();
            _database.Prodcuts.Clear();
        }
    }
}
