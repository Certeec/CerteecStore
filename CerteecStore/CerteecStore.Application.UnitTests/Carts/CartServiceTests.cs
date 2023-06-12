using CerteecStore.Application.Carts;
using CerteecStore.Application.Database;
using CerteecStore.Application.Products;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.UnitTests.Carts
{
    internal class CartServiceTests
    {
        private Product _productForTests;
        private Mock<ICartRepository> _cartRepositoryMocked;
        private Mock<IProductRepository> _productRepositoryMocked;
        private ICartService _serviceUT;
        private int _userId;
        private int _productId;
        private int _quantity;

        [SetUp]
        public void SetUp()
        {
            _cartRepositoryMocked = new Mock<ICartRepository>();
            _productRepositoryMocked = new Mock<IProductRepository>();
            _serviceUT = new CartService(_cartRepositoryMocked.Object, _productRepositoryMocked.Object);
            _userId = 1;
            _productForTests = new Product()
            {
                ProductId = 2,
                Description = "",
                ItemPrice = 1,
                Name = "testId2",
                Quantity = 2
            };
            _productId = 1;
            _quantity = 1;
        }

        [Test]
        public void CountCartValue_Should()
        {
            //Arrange
            

            //Act
            var result = _serviceUT.CountCartValue(_userId);

            //Assert

        }

        [Test]
        public void AddProductToCart_ShouldTrigerInsert_WhenProductDoesntExistInCart()
        {
            //Arrange
            _productRepositoryMocked.Setup(n => n.FindProductById(_productId)).Returns(_productForTests);
            _cartRepositoryMocked.Setup(n => n.GetProductQuantity(_userId, _productId)).Returns((ProductsInCart)null);


            //Act
            var result = _serviceUT.AddProductToCart(_userId, 1, 1);

            //Assert
            _cartRepositoryMocked.Verify(n => n.InsertIntoCart(_userId, _productId, _quantity));

        }


        [Test]
        public void AddProductToCart_ShouldTrigerUPDATE_WhenProductExistInCart()
        {
            //Arrange
            ProductsInCart cart = new ProductsInCart()
            {
                Quantity = 3
            };
            _productRepositoryMocked.Setup(n => n.FindProductById(_productId)).Returns(_productForTests);
            _cartRepositoryMocked.Setup(n => n.GetProductQuantity(_userId, _productId)).Returns(cart);


            //Act
            var result = _serviceUT.AddProductToCart(_userId, 1, 1);

            //Assert
            _cartRepositoryMocked.Verify(n => n.UpdateQuantityInCart(_userId, _productId, It.IsAny<int>()));

        }

        [Test]
        public void AddProductToCart_ShouldReturnZero_WhenProductDoesntExist()
        {
            //Arrange
            _productRepositoryMocked.Setup(n => n.FindProductById(_productId)).Returns((Product)null);

            //Act
            var result = _serviceUT.AddProductToCart(_userId, _productId, _quantity);

            //Assert
            result.Should().Be(0);

        }

        [Test]
        public void AddProductToCart_ShouldReturnZerro_WhenProductQuantityIsLowerThenToAdd()
        {
            //Arrange
            ProductsInCart cart = new ProductsInCart()
            {
                Quantity = 3
            };
            _productRepositoryMocked.Setup(n => n.FindProductById(_productId)).Returns(_productForTests);
            _cartRepositoryMocked.Setup(n => n.GetProductQuantity(_userId, _productId)).Returns(cart);


            //Act
            var result = _serviceUT.AddProductToCart(_userId, 1, 5);

            //Assert
            _cartRepositoryMocked.VerifyNoOtherCalls();
            result.Should().Be(0);

        }


        //        [Test]
        //        public void CountCartValue_ShouldReturnedSumUpOfCart()
        //        {
        //            //Arrange
        //            Cart cartUT = new Cart();
        //            Product productToCount = new Product()
        //            {
        //                ProductId = 1,
        //                Description = "",
        //                ItemPrice = 5,
        //                Name = "test",
        //                Quantity = 1
        //            };

        //            cartUT.Products.Add(productToCount, 3);
        //            _cartRepositoryMocked.Setup(n => n.GetCartByUserId(_userId)).Returns(cartUT);

        //            //Act
        //            var result = _serviceUT.CountCartValue(_userId);

        //            //Assert
        //            result.Should().Be(15);
        //        }



        //        [Test]
        //        public void AddProductToCart_ShouldAddQuantityToProductCount_WhenGivenCartWithExistingProduct()
        //        {
        //            //Arrange
        //            Cart cartExpected = new Cart();
        //            int quantity = 1;
        //            int productId = 1;
        //            cartExpected.Products.Add(_productForTests, 1);
        //            _productRepositoryMocked.Setup(n => n.FindProductById(productId)).Returns(_productForTests);
        //            _cartRepositoryMocked.Setup(n => n.GetCartByUserId(_userId)).Returns(cartExpected);

        //            //Act
        //            var result = _serviceUT.AddProductToCart(_userId, 1, quantity);


        //            //Assert
        //            result.Should().NotBe(false);
        //            _cartRepositoryMocked.Verify(n => n.UpdateCart(_userId, cartExpected));
        //            cartExpected.Products.Should().Contain(_productForTests, 2);
        //        }

        //        [Test]
        //        public void AddProductToCart_ShouldCreateNewCart_WhenCartDoesntExist()
        //        {
        //            //Arrange
        //            int quantity = 1;
        //            int productId = 1;
        //            _productRepositoryMocked.Setup(n => n.FindProductById(productId)).Returns(_productForTests);
        //            _cartRepositoryMocked.Setup(n => n.GetCartByUserId(_userId)).Returns(new Cart());

        //            //Act
        //            var result = _serviceUT.AddProductToCart(_userId, productId, quantity);

        //            //Assert
        //            result.Should().BeTrue();
        //            _cartRepositoryMocked.Verify(n => n.UpdateCart(_userId, It.IsAny<Cart>()));
        //        }

        //        [Test]
        //        public void AddProductToCart_ShouldReturnFalse_WhenProductDoesntExistInDatabase()
        //        {
        //            //Arrange
        //            int quantity = 1;
        //            int productId = 1;
        //            _productRepositoryMocked.Setup(n => n.FindProductById(productId)).Returns<Product>(null);

        //            //Act
        //            var result = _serviceUT.AddProductToCart(_userId, 1, quantity);

        //            //Assert
        //            result.Should().BeFalse();
        //        }


        //        [Test]
        //        public void RemoveOneProductFromTheCart()
        //        {
        //            //Arrange
        //            Cart cartWithOneItem = new Cart();
        //            cartWithOneItem.Products.Add(_productForTests, 2);
        //            _productRepositoryMocked.Setup(n => n.FindProductById(It.IsAny<int>())).Returns(_productForTests);
        //            _cartRepositoryMocked.Setup(n => n.GetCartByUserId(It.IsAny<Guid>())).Returns(cartWithOneItem);

        //            //Act
        //            var result = _serviceUT.RemoveOneProductFromTheCart(_userId, 1);

        //            //Assert
        //            result.Should().NotBe(-1);
        //            _cartRepositoryMocked.Verify(n => n.UpdateCart(_userId, It.Is<Cart>(n => n.Products[_productForTests] == 1)));
        //        }

        //        [Test]
        //        public void RemoveOneProductFromTheCart_ShouldReturnNegativeOne_WhenGivenNullProduct()
        //        {
        //            //Arrange
        //            _productRepositoryMocked.Setup(n => n.FindProductById(It.IsAny<int>())).Returns<Cart>(null);
        //            _cartRepositoryMocked.Setup(n => n.GetCartByUserId(It.IsAny<Guid>())).Returns(new Cart());

        //            //Act
        //            var result = _serviceUT.RemoveOneProductFromTheCart(_userId, 1);

        //            //Assert
        //            result.Should().Be(-1);
        //            _cartRepositoryMocked.VerifyNoOtherCalls();
        //        }
    }
}
