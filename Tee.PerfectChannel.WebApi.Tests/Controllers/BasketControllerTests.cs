using System.Web.Http.Results;
using NSubstitute;
using NUnit.Framework;
using Tee.PerfectChannel.WebApi.Controllers;
using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Services;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tee.PerfectChannel.WebApi.Tests.Controllers
{
    [TestFixture]
    public class BasketControllerTests
    {
        private BasketController controller;
        private IItemService itemService;
        private IMapperService mapperService;
        private IBasketService basketService;
        private IUserService userService;

        [SetUp]
        public void Setup()
        {
            this.itemService = Substitute.For<IItemService>();
            this.mapperService = Substitute.For<IMapperService>();
            this.basketService = Substitute.For<IBasketService>();
            this.userService = Substitute.For<IUserService>();
            this.controller = new BasketController(itemService, mapperService, basketService, userService);
        }

        [Test]
        public void Get_ReturnsBasketOk()
        {
            // Arrange
            this.userService.Get("TestUser").Returns(new User());
            // Act
            var actionResult = controller.GetBasket("TestUser");

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Basket>));
        }

        [Test]
        public void AddItemToBasket_ReturnsBasketOk()
        {
            // Arrange
            var userId = 1;
            var itemId = 22;
            this.basketService.GetByUserId(userId).Returns(new Basket());
            this.mapperService.Map(Arg.Any<Item>()).Returns(new BasketItem());
            this.itemService.Get(itemId).Returns(new Item { Stock = 4 });

            // Act
            var actionResult = controller.AddItemToBasket(userId, itemId, 2);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Basket>));
        }

        [Test]
        public void AddItemToBasket_GivenAvailableStock_AddsItemToBasket()
        {
            // Arrange
            var userId = 1;
            this.basketService.GetByUserId(userId).Returns(new Basket());
            var basketItem = new BasketItem();
            this.mapperService.Map(Arg.Any<Item>()).Returns(basketItem);

            const int itemId = 1;
            const int quantity = 2;

            this.itemService.Get(itemId).Returns(new Item { Stock = 4 });

            // Act
            controller.AddItemToBasket(userId, itemId, quantity);

            // Assert
            this.basketService.Received(1).AddToBasket(userId, basketItem);
            // TODO this.basketService.Received(1).AddToBasket(userId, Arg.Is<BasketItem>(i => i.Id == userId && i.Quantity == quantity));
        }

        [Test]
        public void AddItemToBasket_GivenUnavailableStock_ReturnsBadRequest()
        {
            // Arrange
            var userId = 1;
            this.basketService.GetByUserId(userId).Returns(new Basket());
            var basketItem = new BasketItem();
            this.mapperService.Map(Arg.Any<Item>()).Returns(basketItem);

            const int itemId = 1;
            const int quantity = 2;

            this.itemService.Get(itemId).Returns(new Item { Stock = 1 });

            // Act
            var actionResult = controller.AddItemToBasket(userId, itemId, quantity);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [Test]
        public void AddItemToBasket_GivenUnavailableStock_DoesNotAddItemToBasket()
        {
            // Arrange
            var userId = 1;
            this.basketService.GetByUserId(userId).Returns(new Basket());
            var basketItem = new BasketItem();
            this.mapperService.Map(Arg.Any<Item>()).Returns(basketItem);

            const int itemId = 1;
            const int quantity = 2;

            this.itemService.Get(itemId).Returns(new Item { Stock = 1 });

            // Act
            controller.AddItemToBasket(userId, itemId, quantity);

            // Assert
            this.basketService.Received(0).AddToBasket(Arg.Any<int>(), Arg.Any<BasketItem>());
        }
    }
}