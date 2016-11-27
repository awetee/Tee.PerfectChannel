using System.Web.Http.Results;
using NSubstitute;
using NUnit.Framework;
using Tee.PerfectChannel.WebApi.Controllers;
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

        [SetUp]
        public void Setup()
        {
            this.itemService = Substitute.For<IItemService>();
            this.mapperService = Substitute.For<IMapperService>();
            this.basketService = Substitute.For<IBasketService>();
            this.controller = new BasketController(itemService, mapperService, basketService);
        }

        [Test]
        public void Get_ReturnsBasketOk()
        {
            // Arrange
            // Act
            var actionResult = controller.GetBasket(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Basket>));
        }

        [Test]
        public void AddItemToBasket_ReturnsBasketOk()
        {
            // Arrange
            var basketId = 1;
            this.basketService.Get(basketId).Returns(new Basket());
            this.mapperService.Map(Arg.Any<Item>()).Returns(new BasketItem());

            // Act
            var actionResult = controller.AddItemToBasket(basketId, 1, 2);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Basket>));
        }

        [Test]
        public void AddItemToBasket_AddsItemToBasket()
        {
            // Arrange
            var basketId = 1;
            this.basketService.Get(basketId).Returns(new Basket());
            var basketItem = new BasketItem();
            this.mapperService.Map(Arg.Any<Item>()).Returns(basketItem);

            const int itemId = 1;
            const int quantity = 2;

            // Act
            controller.AddItemToBasket(basketId, itemId, quantity);

            // Assert
            this.basketService.Received(1).AddToBasket(basketId, basketItem);
            // TODO this.basketService.Received(1).AddToBasket(basketId, Arg.Is<BasketItem>(i => i.Id == basketId && i.Quantity == quantity));
        }
    }
}