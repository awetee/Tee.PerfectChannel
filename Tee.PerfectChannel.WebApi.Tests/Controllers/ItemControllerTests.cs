using System.Collections.Generic;
using System.Web.Http.Results;
using FluentAssertions;
using NUnit.Framework;
using Tee.PerfectChannel.WebApi.Controllers;
using Tee.PerfectChannel.WebApi.Models;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tee.PerfectChannel.WebApi.Tests.Controllers
{
    [TestFixture]
    public class ItemControllerTests
    {
        private ItemController controller;

        [SetUp]
        public void Setup()
        {
            this.controller = new ItemController();
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