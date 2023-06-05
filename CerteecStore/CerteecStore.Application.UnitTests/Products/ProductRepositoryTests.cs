using CerteecStore.Application.Carts;
using CerteecStore.Application.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;

namespace CerteecStore.Application.UnitTests.Products
{
    internal class ProductRepositoryTests
    {

        private Product _productForTests;
        private Mock<ICartRepository> _cartRepositoryMocked;
        private Mock<IProductRepository> _productRepositoryMocked;
        private ProductRepository _productRepositoryUT;
        private CartService _serviceUT;

        [SetUp]
        public void SetUp()
        {
            _cartRepositoryMocked = new Mock<ICartRepository>();
            _productRepositoryMocked = new Mock<IProductRepository>();
            _serviceUT = new CartService(_cartRepositoryMocked.Object, _productRepositoryMocked.Object);
            _productRepositoryUT = new ProductRepository();
            _productForTests = new Product()
            {
                ProductId = 2,
                Description = "",
                ItemPrice = 1,
                Name = "testId2",
                Quantity = 2
            };
        }
        

        [Test]
        public void ConnectSQl()
        {
            //repositoryUT.FindProductById(1);
            // repositoryUT.FindProductById(1);
            // repositoryUT.RemoveProductById(1);
            CartRepository repoUT = new CartRepository();
            repoUT.AddItemToCart(1, 3, 2);
        }

        [Test]
        public void CheckingFunctiom()
        {
            CartRepository repositoryUT = new CartRepository();
            repositoryUT.ShowAllProductsInCart(1);
            _serviceUT.ShowProductsInCart(1);
     
            
        }

        [Test]
        public void FindProductById_ShouldReturnProduct_WhenGivenExistingId()
        {
            //Arrange

            //Act
            var result =_productRepositoryUT.FindProductById(1);

            //Assert
            result.Should().NotBeNull();
        }

        [Test]
        public void ReadProductsByArray_ShouldReturnListOfProducts()
        {
            //Arrange
            ProductRepository repoUT = new ProductRepository();
            int[] testArray = new int[] { 1, 3, 2, 4 };

            //Act
            var result = repoUT.ReadProductsByArray(testArray);

            //Assert
            result.Should().NotBeEmpty();
        }
    }
}
