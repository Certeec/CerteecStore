using CerteecStore.Application.Products;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.IntegrationTests.Products
{
    internal class DapperProductRepositoryIntergrationTests
    {
        Mock<IConfiguration> _confi;
        DapperProductRepository _repoUT;
        [SetUp]
        public void SetUP()
        {
            _confi = new Mock<IConfiguration>();
            string any = "Data Source=(local)\\SQlEXPRESS; Initial Catalog=Shop; Integrated Security = True; Trusted_Connection=True;TrustServerCertificate=True;";
            _confi.SetupGet(x => x[It.Is<string>(s => s == "ConnectionString")]).Returns(any);
            _repoUT = new DapperProductRepository(_confi.Object);
        }

        [Test]
        public void ReadAllProducts_Should()
        {
          var result =   _repoUT.ReadAllProducts();

        }

        [Test]
        public void RemoveProductById_Should()
        {
            _repoUT.RemoveProductById(12);
        }

        [Test]
        public void FindProductById()
        {
            var result = _repoUT.FindProductById(15);

            result.Should().BeNull();

        }

        [Test]
        public void AddProduct_Should()
        {
            Product productToadd = new Product()
            {
                ProductId = 0,
                Description = "Test",
                Name = "test",
                ItemPrice = 3,
                Quantity = 2
            };
            var result = _repoUT.AddProduct(productToadd);
            result.Should().Be(1);
        }

        [Test]
        public void ReadProductsByArray()
        {
            int[] anyArray = new int[] { 1, 2, 3, 4, 5 };
            _repoUT.ReadProductsByArray(anyArray);
        }
    }
}
