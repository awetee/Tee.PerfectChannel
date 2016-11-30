using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Tee.PerfectChannel.WebApi.Entities;
using Tee.PerfectChannel.WebApi.Repository;
using Tee.PerfectChannel.WebApi.Services;

namespace Tee.PerfectChannel.WebApi.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private IRepository<User> _userRepository;
        private UserService _sut;

        [SetUp]
        public void Setup()
        {
            this._userRepository = Substitute.For<IRepository<User>>();
            this._sut = new UserService(_userRepository);
        }

        [Test]
        public void Get_ReturnsItem()
        {
            // Arrange

            var userName = "Test User";
            _userRepository.GetAll().Returns(new List<User>
            {
                new User { Name = userName},
                new User { Name = "Another User"},
            });

            // Act
            var result = _sut.Get(userName);

            // Assert
            result.Should().NotBeNull();
            result.Name.ShouldBeEquivalentTo(userName);
        }

        [Test]
        public void Get_GivenANullUserName_ThrowsNulArgumentException()
        {
            // Arrange
            // Act
            Assert.Throws<ArgumentNullException>(() => this._sut.Get(null));

            // Assert
            this._userRepository.Received(0).GetAll();
        }
    }
}