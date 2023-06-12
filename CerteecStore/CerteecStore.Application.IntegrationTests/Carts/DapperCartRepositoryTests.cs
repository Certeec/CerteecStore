
using CerteecStore.Application.Carts;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.IntegrationTests.Carts
{
    internal class DapperCartRepositoryTests
    {
        Mock<IConfiguration> _confi;
        DapperCartRepository _repoUT;
        [SetUp]
        public void SetUP()
        {
            _confi = new Mock<IConfiguration>();
            string any = "Data Source=(local)\\SQlEXPRESS; Initial Catalog=Shop; Integrated Security = True; Trusted_Connection=True;TrustServerCertificate=True;";
            _confi.SetupGet(x => x[It.Is<string>(s => s == "ConnectionString")]).Returns(any);
            _repoUT = new DapperCartRepository(_confi.Object);
        }

        [Test]
        public void ShowAllProductsInCart_Should()
        {
 

            _repoUT.ShowAllProductsInCart(1);
        }

        [Test]
        public void GetProductQuantity_should()
        {


            _repoUT.GetProductQuantity(4, 2);
        }

        [Test]
        public void Remove()
        {
            var result = _repoUT.RemoveProductFromCart(4, 3);

            result.Should().Be(1);
        }

        [Test]
        public void InsertIntoCart()
        {
            var result = _repoUT.InsertIntoCart(4, 6, 2);

            result.Should().Be(1);
        }

        [Test]
        public void UpdateQuantityInCart()
        {
            var result = _repoUT.UpdateQuantityInCart(4, 2, 2);

            result.Should().Be(1);
        }
    }
}
