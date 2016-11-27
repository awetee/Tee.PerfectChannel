using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Tee.PerfectChannel.WebApi.Models;
using Tee.PerfectChannel.WebApi.Services;

namespace Tee.PerfectChannel.WebApi.Tests.Services
{
    [TestFixture]
    internal class BasketServiceTests
    {
        private BasketService service;
        private IDataService<Basket> dataService;

        [SetUp]
        public void Setup()
        {
            this.dataService = Substitute.For<IDataService<Basket>>();
            this.service = new BasketService(dataService);
        }

        [Test]
        public void AddToBasket_AddsItemToBasket()
        {
            // Arrange
            var basketId = 2;
            var itemId = 1;

            var basket = new Basket { Id = basketId };

            this.dataService.Get(basketId).Returns(basket);

            // Act
            var result = this.service.AddToBasket(basketId, new BasketItem { ItemId = itemId, Quantity = 2 });

            // Assert
            result.Id.ShouldBeEquivalentTo(basketId);
            result.BasketItems.Count().ShouldBeEquivalentTo(1);
            result.BasketItems.First().ItemId.ShouldBeEquivalentTo(itemId);
        }

        [Test]
        public void AddToBasket_DoesNotDuplicateExistingItem()
        {
            // Arrange
            var basketId = 2;
            var itemId = 1;
            var basket = new Basket { Id = basketId };
            basket.AddBacketItem(new BasketItem { ItemId = itemId, Quantity = 1 });
            basket.AddBacketItem(new BasketItem { ItemId = 2, Quantity = 1 });

            this.dataService.Get(basketId).Returns(basket);

            // Act
            var result = this.service.AddToBasket(basketId, new BasketItem { ItemId = itemId, Quantity = 2 });

            // Assert
            result.BasketItems.Count().ShouldBeEquivalentTo(2);
            result.BasketItems.First(i => i.ItemId == itemId).ItemId.ShouldBeEquivalentTo(itemId);
            result.BasketItems.First(i => i.ItemId == itemId).Quantity.ShouldBeEquivalentTo(3);
            result.BasketItems.First(i => i.ItemId == 2).Quantity.ShouldBeEquivalentTo(1);
        }
    }
}