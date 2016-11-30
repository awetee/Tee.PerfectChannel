using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Repository;
using Tee.PerfectChannel.WebApi.Services;

namespace Tee.PerfectChannel.WebApi.Tests.Services
{
    [TestFixture]
    public class ItemServiceTests
    {
        private ItemService _sut;
        private IRepository<Item> _itemRepository;

        [SetUp]
        public void Setup()
        {
            this._itemRepository = Substitute.For<IRepository<Item>>();
            this._sut = new ItemService(_itemRepository);
        }

        [Test]
        public void GetAll_ReturnsListOfItems()
        {
            // Arrange
            _itemRepository.GetAll().Returns(new List<Item>
            {
                new Item(),
                new Item()
            });

            // Act
            var result = _sut.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Count().ShouldBeEquivalentTo(2);
        }

        [Test]
        public void Get_ReturnsItem()
        {
            // Arrange

            var itemId = 1;
            _itemRepository.Get(itemId).Returns(new Item { Id = itemId });

            // Act
            var result = _sut.Get(itemId);

            // Assert
            result.Should().NotBeNull();
            result.Id.ShouldBeEquivalentTo(itemId);
        }
    }
}