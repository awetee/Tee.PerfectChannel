using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Tee.PerfectChannel.WebApi.Entities;

namespace Tee.PerfectChannel.WebApi.Tests.Entities
{
    internal class BasketTests
    {
        private Basket sut;

        [SetUp]
        public void Setup()
        {
            this.sut = new Basket();
        }

        [Test]
        public void AddToBasket_AddsItemToBasket()
        {
            // Arrange
            const int itemId = 1;

            // Act
            this.sut.Add(new BasketItem { ItemId = itemId, Quantity = 2 });

            // Assert
            sut.BasketItems.Count().ShouldBeEquivalentTo(1);
            sut.BasketItems.First().ItemId.ShouldBeEquivalentTo(itemId);
        }

        [Test]
        public void AddToBasket_DoesNotDuplicateExistingItem()
        {
            // Arrange
            const int itemId = 1;
            this.sut.Add(new BasketItem { ItemId = itemId, Quantity = 1 });
            this.sut.Add(new BasketItem { ItemId = 2, Quantity = 4 });

            // Act
            this.sut.Add(new BasketItem { ItemId = itemId, Quantity = 2 });

            // Assert
            sut.BasketItems.Count().ShouldBeEquivalentTo(2);
            sut.BasketItems.First(i => i.ItemId == itemId).ItemId.ShouldBeEquivalentTo(itemId);
            sut.BasketItems.First(i => i.ItemId == itemId).Quantity.ShouldBeEquivalentTo(3);
            sut.BasketItems.First(i => i.ItemId == 2).Quantity.ShouldBeEquivalentTo(4);
        }
    }
}