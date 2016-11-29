using System;
using System.Collections.Generic;
using FluentAssertions.Common;
using NSubstitute;
using NUnit.Framework;
using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Repository;
using Tee.PerfectChannel.WebApi.Services;

namespace Tee.PerfectChannel.WebApi.Tests.Services
{
    [TestFixture]
    internal class BasketServiceTests
    {
        private BasketService service;
        private IRepository<Basket> dataService;

        [SetUp]
        public void Setup()
        {
            this.dataService = Substitute.For<IRepository<Basket>>();
            this.service = new BasketService(dataService);
        }

        [Test]
        public void GetByUserId_ReturnsBasket()
        {
            // Arrange
            var userId = 2;

            var basket = new Basket { UserId = userId };

            this.dataService.GetAll().Returns(new List<Basket> { basket });

            // Act
            var result = this.service.GetByUserId(userId);

            // Assert
            result.IsSameOrEqualTo(basket);
        }

        [Test]
        public void Update_GivenAValidBasket_UpdatesBasket()
        {
            // Arrange
            var basket = new Basket();

            // Act
            this.service.Update(basket);

            // Assert
            this.dataService.Received(1).Update(basket);
        }

        [Test]
        public void Update_GivenANullBasket_DoesNotUpdatesBasket()
        {
            // Arrange
            // Act
            Assert.Throws<ArgumentNullException>(() => this.service.Update(null));

            // Assert
            this.dataService.Received(0).Update(Arg.Any<Basket>());
        }
    }
}