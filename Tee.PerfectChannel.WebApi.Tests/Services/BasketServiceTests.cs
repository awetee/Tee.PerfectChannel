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
        private IRepository<Basket> basketRepository;
        private IRepository<Invoice> invoiceRepository;
        private IRepository<InvoiceItem> invoiceItemRepository;
        private IRepository<BasketItem> basketItemRepository;

        [SetUp]
        public void Setup()
        {
            this.basketRepository = Substitute.For<IRepository<Basket>>();
            this.invoiceRepository = Substitute.For<IRepository<Invoice>>();
            this.invoiceItemRepository = Substitute.For<IRepository<InvoiceItem>>();
            this.basketItemRepository = Substitute.For<IRepository<BasketItem>>();
            this.service = new BasketService(basketRepository, invoiceRepository, invoiceItemRepository, basketItemRepository);
        }

        [Test]
        public void GetByUserId_ReturnsBasket()
        {
            // Arrange
            var userId = 2;

            var basket = new Basket { UserId = userId };

            this.basketRepository.GetAll().Returns(new List<Basket> { basket });

            // Act
            var result = this.service.GetByUserId(userId);

            // Assert
            result.UserId.IsSameOrEqualTo(userId);
        }

        [Test]
        public void Update_GivenAValidBasket_UpdatesBasket()
        {
            // Arrange
            var basket = new Basket();

            // Act
            this.service.Update(basket);

            // Assert
            this.basketRepository.Received(1).Update(basket);
        }

        [Test]
        public void Checkout_Creates_Invoice()
        {
            // Arrange
            var userId = 1;
            var basket = new Basket { UserId = userId };

            // Act
            this.service.Checkout(basket);

            // Assert
            this.invoiceRepository.Received(1).Insert(Arg.Any<Invoice>());
        }

        [Test]
        public void Checkout_AddsInvoiceItems()
        {
            // Arrange
            var basket = new Basket();
            basket.Add(new BasketItem { ItemId = 1, Quantity = 2, Price = 23 });

            // Act
            this.service.Checkout(basket);

            // Assert
            this.invoiceItemRepository.Received(1).Insert(Arg.Any<InvoiceItem>());
        }

        [Test]
        public void Checkout_Deletes_BasketItems()
        {
            // Arrange
            var basket = new Basket();
            basket.Add(new BasketItem { ItemId = 1, Quantity = 2, Price = 23 });

            // Act
            this.service.Checkout(basket);

            // Assert
            this.basketItemRepository.Received(1).Delete(Arg.Any<BasketItem>());
        }

        [Test]
        public void Checkout_Returns_Invoice()
        {
            // Arrange
            var basket = new Basket();
            basket.Add(new BasketItem { ItemId = 1, Quantity = 2, Price = 23 });

            this.invoiceRepository.Insert(Arg.Any<Invoice>()).Returns(1);
            var invoice = new Invoice();
            this.invoiceRepository.Get(1).Returns(invoice);

            // Act
            var result = this.service.Checkout(basket);

            // Assert
            result.IsSameOrEqualTo(invoice);
        }

        [Test]
        public void Update_GivenANullBasket_DoesNotUpdatesBasket()
        {
            // Arrange
            // Act
            Assert.Throws<ArgumentNullException>(() => this.service.Update(null));

            // Assert
            this.basketRepository.Received(0).Update(Arg.Any<Basket>());
        }
    }
}