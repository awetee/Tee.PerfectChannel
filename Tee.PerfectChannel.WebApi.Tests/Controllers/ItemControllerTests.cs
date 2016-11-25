using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FluentAssertions;
using NUnit.Framework;
using Tee.PerfectChannel.WebApi.Controllers;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tee.PerfectChannel.WebApi.Tests.Controllers
{
    [TestFixture]
    public class ItemControllerTests
    {
        [Test]
        public void Get_Returns_Ok()
        {
            // Arrange
            var controller = new ItemController();

            // Act
            var actionResult = controller.Get();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<Item>>));
        }

        [Test]
        public void Get_Returns_IEnuerableOfItem()
        {
            // Arrange
            var controller = new ItemController();

            // Act
            var actionResult = controller.Get();

            // Assert
            var result = actionResult as OkNegotiatedContentResult<IEnumerable<Item>>;
            result.Content.Should().NotBeNull();
            result.Content.Count().ShouldBeEquivalentTo(2);
        }
    }
}