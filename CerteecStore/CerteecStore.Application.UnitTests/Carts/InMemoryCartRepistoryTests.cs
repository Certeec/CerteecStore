//using CerteecStore.Application.Carts;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CerteecStore.Application.Carts;
//using CerteecStore.Application.Database;
//using FluentAssertions;

//namespace CerteecStore.Application.UnitTests.Carts
//{
//    internal class InMemoryCartDepistoryTests
//    {
//        InMemoryCartRepository _repositoryUT;

//        [SetUp]
//        public void SetUp()
//        {
//            _repositoryUT = new InMemoryCartRepository();
//        }


//        [Test]
//        public void FindOrCreateCartByUserId_ShouldReturnCart_WhenGuidMatches()
//        {
//            //Arrange
//            Guid userId = Guid.NewGuid();
//            Cart userCart = new Cart();
//            InMemoryDatabase.Carts.Add(userId, userCart);

//            //Act
//            Cart result = _repositoryUT.GetCartByUserId(userId);

//            //Assert
//            result.Should().BeSameAs(userCart);

//        }

//        [Test]
//        public void FindOrCreateCartByUserId_ShouldReturnEmptyCart_WhenGuidIsWrong()
//        {
//            //Arrange
//            Guid userId = Guid.NewGuid();
//            Cart userCart = new Cart();
//            InMemoryDatabase.Carts.Add(Guid.Empty, userCart);

//            //Act
//            Cart result = _repositoryUT.GetCartByUserId(userId);

//            //Assert
//            result.Should().BeNull();
//            result.Should().NotBeSameAs(userCart);

//        }

//        [Test]
//        public void UpdateCart_ShouldSetSameCart_AsParameterSet()
//        {
//            //Arrange
//            Guid userId = Guid.NewGuid();
//            Cart firstCart = new Cart();
//            InMemoryDatabase.Carts.Add(userId, firstCart);
//            Cart expectedCart = new Cart();

//            //Act
//            _repositoryUT.UpdateCart(userId, expectedCart);

//            //Assert
//            Cart userCart = InMemoryDatabase.Carts.First(n => n.Key == userId).Value;
//            InMemoryDatabase.Carts[userId].Should().BeSameAs(expectedCart);
//            userCart.Should().BeSameAs(expectedCart);
//        }


//        [TearDown]
//        public void TearDown()
//        {
//            InMemoryDatabase.Carts.Clear();
//            InMemoryDatabase.Prodcuts.Clear();
//        }
//    }
//}
