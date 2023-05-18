using CerteecStore.Application.Carts;
using CerteecStore.Application.Database;
using CerteecStore.Application.Products;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CerteecStore.Application.UnitTests.Cart
{
    internal class CartServiceTests
    {
        ICartRepository _cartRepository;
        IProductRepository _productRepository;
        CartService _cartServiceUT;
       
        [SetUp]
        public void SetUp()
        {
            _cartRepository = new InMemoryCartRepository();
            _productRepository = new InMemoryProductRepository();
            CartService _cartServiceUT = new CartService(_cartRepository, _productRepository);
        }
        [Test]
        public void AddProductToCart_ShoulHaveOneProductOfQuanityOne_WhenAddingOneNonExistingItemToCart()
        {
            //Arrange
            InMemoryCartRepository memoryRepo = new InMemoryCartRepository();
            CartService currentServiceUT = new CartService(memoryRepo, _productRepository);
            Guid user = Guid.NewGuid();
            int productId = 1;
            int quantityToADd = 1;
            InMemoryDatabase.Prodcuts.Add(new Product()
            {
                ProductId = 1,
                Description = "none",
                ItemPrice = 1,
                Name = "Product",
                Quantity = 1
            });


            //Act
            currentServiceUT.AddProductToCart(user, 1, 1);

            //Assert
            InMemoryDatabase.Carts[user].Products.Should().ContainValue(1);


        }

        [TearDown]
        public void TearDown()
        {
            InMemoryDatabase.Carts.Clear();
            InMemoryDatabase.Prodcuts.Clear();
           
        }
    }
}
