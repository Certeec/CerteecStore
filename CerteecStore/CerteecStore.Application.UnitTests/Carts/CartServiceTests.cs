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
        ICartRepository _cartRepository;
        IProductService _productService;
        IProductRepository _productRepository;
        InMemoryDatabase _database;
        ICartService _cartService;
        Product _productForTests;


        [SetUp]
        public void SetUp()
        {
            _database = new InMemoryDatabase();
            _cartRepository = new InMemoryCartRepository(_database);
            _productRepository = new InMemoryProductRepository(_database);
            _productService = new ProductService(_productRepository);
            _cartService = new CartService(_cartRepository, _productService);
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
        public void CountCartValue_ShouldReturnedSumUpOfCart()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Cart cartUT = new Cart();
            Product productToCount = new Product()
            {
                ProductId = 1,
                Description = "",
                ItemPrice = 5,
                Name = "test",  
                Quantity = 1
            };

            cartUT.Products.Add(productToCount, 3);
            var cartRepostioryMocked = new Mock<ICartRepository>();
            CartService serviceUT = new CartService(cartRepostioryMocked.Object, _productService);
            cartRepostioryMocked.Setup(n => n.GetCartByUserId(userId)).Returns(cartUT);

            //Act
            var result = serviceUT.CountCartValue(userId);

            //Assert
            result.Should().Be(15);
        }

        [Test]
        public void AddProductToCart_ShouldAddNewProductToCart_WhenGivenCartWithoutExistingProduct()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Cart cartExpected = new Cart();
            int quantity = 1;
            int productId = 1;
            var cartRepositoryMocked = new Mock<ICartRepository>();
            var productServiceMocked = new Mock<IProductService>();
            productServiceMocked.Setup(n => n.FindProductById(productId)).Returns(_productForTests);
            cartRepositoryMocked.Setup(n => n.GetCartByUserId(userId)).Returns(cartExpected);
            CartService serviceUT = new CartService(cartRepositoryMocked.Object, productServiceMocked.Object);

            //Act
            var result = serviceUT.AddProductToCart(userId, 1, quantity);

            //Assert
            result.Should().NotBe(false);
            cartRepositoryMocked.Verify(n => n.UpdateCart(userId, cartExpected));
            cartExpected.Products.Should().Contain(_productForTests, quantity);
        }

        [Test]
        public void AddProductToCart_ShouldAddQuantityToProductCount_WhenGivenCartWithExistingProduct()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            Cart cartExpected = new Cart();
            int quantity = 1;
            int productId = 1;
            cartExpected.Products.Add(_productForTests, 1);
            var cartRepositoryMocked = new Mock<ICartRepository>();
            var productServiceMocked = new Mock<IProductService>();
            productServiceMocked.Setup(n => n.FindProductById(productId)).Returns(_productForTests);
            cartRepositoryMocked.Setup(n => n.GetCartByUserId(userId)).Returns(cartExpected);
            CartService serviceUT = new CartService(cartRepositoryMocked.Object, productServiceMocked.Object);

            //Act
            var result = serviceUT.AddProductToCart(userId, 1, quantity);


            //Assert
            result.Should().NotBe(false);
            cartRepositoryMocked.Verify(n => n.UpdateCart(userId, cartExpected));
            cartExpected.Products.Should().Contain(_productForTests, 2);
        }

        [Test]
        public void AddProductToCart_ShouldCreateNewCart_WhenCartDoesntExist()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            int quantity = 1;
            int productId = 1;
            var cartRepositoryMocked = new Mock<ICartRepository>();
            var productServiceMocked = new Mock<IProductService>();
            productServiceMocked.Setup(n => n.FindProductById(productId)).Returns(_productForTests);
            cartRepositoryMocked.Setup(n => n.GetCartByUserId(userId)).Returns(new Cart());
            CartService serviceUT = new CartService(cartRepositoryMocked.Object, productServiceMocked.Object);

            //Act
            var result = serviceUT.AddProductToCart(userId, 1, quantity);

            //Assert
            result.Should().NotBe(false);
            cartRepositoryMocked.Verify(n => n.UpdateCart(userId, It.IsAny<Cart>()));
        }

        [Test]
        public void AddProductToCart_ShouldReturnFalse_WhenProductDoesntExistInDatabase()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            int quantity = 1;
            int productId = 1;
            var cartRepositoryMocked = new Mock<ICartRepository>();
            var productServiceMocked = new Mock<IProductService>();
            productServiceMocked.Setup(n => n.FindProductById(productId)).Returns<Product>(null);
            CartService serviceUT = new CartService(cartRepositoryMocked.Object, productServiceMocked.Object);

            //Act
            var result = serviceUT.AddProductToCart(userId, 1, quantity);

            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public void CreateCart_ShouldCreateNewCart()
        {
            //Arrange
            var cartRepositoryMocked = new Mock<ICartRepository>();
            CartService serviceUt = new CartService(cartRepositoryMocked.Object, _productService);

            //Act
            var result = serviceUt.CreateCart(Guid.NewGuid());

            //Assert
            result.Should().BeOfType<Cart>();
        }

        [Test]
        public void RemoveOneProductFromTheCart()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            var productServiceMocked = new Mock<IProductService>();
            var cartRepositoryMocked = new Mock<ICartRepository>();
            int productId = 1;
            Cart cartWithOneItem = new Cart();
            cartWithOneItem.Products.Add(_productForTests, 2);
            CartService serviceUT = new CartService(cartRepositoryMocked.Object, productServiceMocked.Object);
            productServiceMocked.Setup(n => n.FindProductById(productId)).Returns(_productForTests);


            //Act
           var result =  serviceUT.RemoveOneProductFromTheCart(userId, 1);

            //Assert
            result.Should().NotBe(-1);
            cartRepositoryMocked.Verify(n => n.UpdateCart(userId, cartWithOneItem));
            cartWithOneItem.Products.Should().Contain(_productForTests, 1);
        }

        [TearDown]
        public void TearDown()
        {


        }
    }
}
