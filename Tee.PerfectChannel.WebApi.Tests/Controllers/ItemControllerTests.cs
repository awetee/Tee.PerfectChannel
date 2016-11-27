using System.Collections.Generic;
using System.Web.Http.Results;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Tee.PerfectChannel.WebApi.Controllers;
using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Services;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tee.PerfectChannel.WebApi.Tests.Controllers
{
    [TestFixture]
    public class ItemControllerTests
    {
        private ItemController controller;
        private IItemService itemService;

        [SetUp]
        public void Setup()
        {
            this.itemService = Substitute.For<IItemService>();
            this.controller = new ItemController(itemService);
        }

        [Test]
        public void Get_Returns_Ok()
        {
            // Arrange

            // Act
            var actionResult = controller.Get();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<Item>>));
        }

        [Test]
        public void Get_Returns_IEnuerableOfItem()
        {
            // Arrange

            // Act
            var actionResult = controller.Get();

            // Assert
            var result = actionResult as OkNegotiatedContentResult<IEnumerable<Item>>;
            result.Content.Should().NotBeNull();
        }
    }
}