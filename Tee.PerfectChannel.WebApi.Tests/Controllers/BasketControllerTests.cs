using System.Web.Http.Results;
using NUnit.Framework;
using Tee.PerfectChannel.WebApi.Controllers;
using Tee.PerfectChannel.WebApi.Models;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tee.PerfectChannel.WebApi.Tests.Controllers
{
    [TestFixture]
    public class BasketControllerTests
    {
        private BasketController controller;

        [SetUp]
        public void Setup()
        {
            this.controller = new BasketController();
        }

        [Test]
        public void Get_Returns_Ok()
        {
            // Arrange

            // Act
            var actionResult = controller.GetBasket();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Basket>));
        }
    }
}