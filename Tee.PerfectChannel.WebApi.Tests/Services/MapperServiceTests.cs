using FluentAssertions;
using NUnit.Framework;
using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Services;

namespace Tee.PerfectChannel.WebApi.Tests.Services
{
    [TestFixture]
    public class MapperServiceTests
    {
        [Test]
        public void MapItemToBasketItem_ReturnBasketItem()
        {
            // Arrange
            var service = new MapperService();
            var item = new Item
            {
                Id = 1,
                Name = "TestBasket",
                Description = "Test Basket Description",
                Price = 45,
                Stock = 44
            };

            // Act
            var result = service.Map(item);

            // Assert
            result.ItemName.ShouldBeEquivalentTo(item.Name);
            result.ItemId.ShouldBeEquivalentTo(item.Id);
            result.Price.ShouldBeEquivalentTo(item.Price);
        }
    }
}