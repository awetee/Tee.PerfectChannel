using System.Linq;
using System.Web.Http.Results;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Tee.PerfectChannel.WebApi.Controllers;
using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Models;
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
        public void GetBasket_ReturnsBasketOk()
        {
            // Arrange
            this.userService.Get("TestUser").Returns(new User());
            // Act
            var actionResult = controller.GetBasket("TestUser");

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Basket>));
        }

        [Test]
        public void GetBasket_GivenAnUnknownUserName_ReturnsBasketOk()
        {
            // Arrange
            this.userService.Get("TestUser").Returns(new User());
            // Act
            var actionResult = controller.GetBasket("Unknown User");

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [Test]
        public void AddItemToBasket_ReturnsBasketOk()
        {
            // Arrange
            const int userId = 1;
            const int itemId = 22;
            this.basketService.GetByUserId(userId).Returns(new Basket());
            this.mapperService.Map(Arg.Any<Item>()).Returns(new BasketItem());
            this.itemService.Get(itemId).Returns(new Item { Stock = 4 });

            // Act
            var actionResult = controller.AddBasketEntry(userId, new BasketEntry { ItemId = itemId, Quantity = 2 });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Basket>));
        }

        [Test]
        public void AddItemToBasket_GivenAvailableStock_AddsItemToBasket()
        {
            // Arrange
            const int userId = 1;
            this.basketService.GetByUserId(userId).Returns(new Basket());
            var basketItem = new BasketItem();
            this.mapperService.Map(Arg.Any<Item>()).Returns(basketItem);

            const int itemId = 1;
            const int quantity = 2;

            this.itemService.Get(itemId).Returns(new Item { Stock = 4 });

            // Act
            var actionResult = controller.AddBasketEntry(userId, new BasketEntry { ItemId = itemId, Quantity = quantity });

            // Assert
            var result = actionResult as OkNegotiatedContentResult<Basket>;
            result.Content.BasketItems.Count().ShouldBeEquivalentTo(1);
            result.Content.BasketItems.First().ShouldBeEquivalentTo(basketItem);
            this.basketService.Received(1).Update(Arg.Any<Basket>());
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
            var actionResult = controller.AddBasketEntry(userId, new BasketEntry { ItemId = itemId, Quantity = quantity });

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
            controller.AddBasketEntry(userId, new BasketEntry { ItemId = itemId, Quantity = quantity });

            // Assert
            this.basketService.Received(0).Update(Arg.Any<Basket>());
        }

        [Test]
        public void Checkout_ReturnsInvoiceOk()
        {
            // Arrange
            var userId = 1;
            this.userService.Get("TestUser").Returns(new User { Id = userId });

            var basket = new Basket();
            basket.Add(new BasketItem { ItemId = 2 });

            this.basketService.GetByUserId(userId).Returns(basket);
            this.itemService.Get(2).Returns(new Item { Id = 2, Stock = 4 });

            // Act
            var actionResult = controller.Checkout("TestUser");

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Invoice>));
        }

        [Test]
        public void Checkout_ChecksOut()
        {
            // Arrange
            var userId = 1;
            this.userService.Get("TestUser").Returns(new User { Id = userId });

            var basket = new Basket();
            basket.Add(new BasketItem { ItemId = 2 });

            this.basketService.GetByUserId(userId).Returns(basket);
            this.itemService.Get(2).Returns(new Item { Id = 2, Stock = 4 });

            // Act
            controller.Checkout("TestUser");

            // Assert
            this.basketService.Received(1).Checkout(basket);
        }
    }
}