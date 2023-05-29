//using CerteecStore.Application.Carts;
//using CerteecStore.Application.Database;
//using CerteecStore.Application.Products;
//using FluentAssertions;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CerteecStore.Application.UnitTests.Carts
//{
//    internal class CartServiceTests
//    {
//        ICartRepository _cartRepository;
//        IProductService _productService;
//        IProductRepository _productRepository;
//        InMemoryDatabase _database;
//        ICartService _cartService;


//        [SetUp]
//        public void SetUp()
//        {
//            _database = new InMemoryDatabase();
//            _cartRepository = new InMemoryCartRepository(_database);
//            _productRepository = new InMemoryProductRepository(_database);
//            _productService = new ProductService(_productRepository);
//            _cartService = new CartService(_cartRepository, _productService);
//        }
//        [Test]
//        public void AddProductToCart_ShoulHaveOneProductOfQuanityOne_WhenAddingOneNonExistingItemToCart()
//        {
//            //Arrange
//            InMemoryCartRepository memoryRepo = new InMemoryCartRepository(_database);
//            CartService currentServiceUT = new CartService(memoryRepo, _productService);
//            Guid user = Guid.NewGuid();
//            int productId = 1;
//            int quantityToADd = 1;
//            //Product product = new Product()
//            //{
//            //    ProductId = 1,
//            //    Description = "none",
//            //    ItemPrice = 1,
//            //    Name = "Product",
//            //    Quantity = 1
//            //};
//            //_database.Prodcuts.Add(new Product()
//            //{
//            //    ProductId = 1,
//            //    Description = "none",
//            //    ItemPrice = 1,
//            //    Name = "Product",
//            //    Quantity = 1
//            //});



//            //Act
//            currentServiceUT.AddProductToCart(user, 1, 1);
//            // may throw error after recent changes with method OF Finding product id.

//            //Assert
//            _database.Carts[user].Products.Should().ContainValue(1);

//        }

//        [Test]
//        public void CountCartValue_ShouldReturnedSumUpOfCart()
//        {
//            //Arrange
//            Guid userId = Guid.NewGuid();
//            Cart cartExpected = new Cart();
//            Product productExpected = new Product()
//            {
//                ProductId = 1,
//                Description = "",
//                ItemPrice = 1,
//                Name = "test",
//                Quantity = 1
//            };

//            cartExpected.Products.Add(productExpected, 3);

//            var serviceRepostioryMocked = new Mock<ICartService>();
//            CartService serviceUT = new CartService(serviceRepostioryMocked, _productService);
//            serviceRepostioryMocked.Setup(n => n.FindCartByUserId(userId)).Returns(cartExpected);
            


//            //Act
//            var result = serviceMocked.CountCartValue(userId);

//            //Assert
//            result.Should().Be(3);
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            _database.Carts.Clear();
//            _database.Prodcuts.Clear();

//        }
//    }
//}
