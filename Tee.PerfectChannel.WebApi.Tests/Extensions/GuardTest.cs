using System;
using NUnit.Framework;
using Tee.PerfectChannel.WebApi.Extensions;

namespace Tee.PerfectChannel.WebApi.Tests.Extensions
{
    [TestFixture]
    public class GuardTests
    {
        [Test]
        public void AgainstNullTest()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                Guard.AgainstNull(null, "Null");
            });
        }
    }
}