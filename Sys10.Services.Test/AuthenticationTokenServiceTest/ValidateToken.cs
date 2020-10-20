using Moq;
using Sys10.Data.Helper;
using Sys10.Data.Models;
using Sys10.Data.UnitOfWork;
using Sys10.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Sys10.Services.Test.AuthenticationTokenServiceTest
{
    public class ValidateToken
    {
        [Fact]
        public void WhenTokenAndExpirationIsValid_ShouldReturnTrue()
        {
            //Arrange
            var user = new User()
            {
                AuthenticationToken = "111111",
                AuthenticationTokenExpiration = DateTime.Now.Brasilia().AddDays(1)
            };
            var userList = new List<User>() { user };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(moq => moq.RepositoryBase.Get<User>(
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>()))
                .Returns(userList.AsQueryable());
            var service = new AuthenticationTokenService(mockUnitOfWork.Object);

            //Act
            var result = service.ValidateToken("", "111111");

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Status);
        }

        [Fact]
        public void WhenTokenIsExpired_ShouldReturnFalse()
        {
            //Arrange
            var user = new User()
            {
                AuthenticationToken = "111111",
                AuthenticationTokenExpiration = DateTime.Now.Brasilia().AddDays(-5)
            };
            var userList = new List<User>() { user };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(moq => moq.RepositoryBase.Get<User>(
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>()))
                .Returns(userList.AsQueryable());
            var service = new AuthenticationTokenService(mockUnitOfWork.Object);

            //Act
            var result = service.ValidateToken("", "111111");

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Status);
        }

        [Theory]
        [InlineData("111111", "999999")]
        [InlineData("123456", "654321")]
        public void WhenTokenIsDifferent_ShouldReturnFalse(string correctToken, string sentToken)
        {
            //Arrange
            var user = new User()
            {
                AuthenticationToken = correctToken,
                AuthenticationTokenExpiration = DateTime.Now.Brasilia().AddDays(-5)
            };
            var userList = new List<User>() { user };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(moq => moq.RepositoryBase.Get<User>(
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>()))
                .Returns(userList.AsQueryable());
            var service = new AuthenticationTokenService(mockUnitOfWork.Object);

            //Act
            var result = service.ValidateToken("", sentToken);

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Status);
        }
    }
}
