using Moq;
using Sys10.Data.Models;
using Sys10.Data.UnitOfWork;
using Sys10.Services.Services;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Sys10.Services.TestServiceTest
{
    public class Authenticate
    {
        [Fact]
        public void WhenUserIsUnexistent_ShouldReturnFalse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(moq => moq.RepositoryBase.FirstOrDefault<User>(
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>()))
                .Returns<User>(null);

            var mockCryptService = new Mock<ICryptService>();
            var mockAuthenticationTokenService = new Mock<IAuthenticationTokenService>();

            var service = new AuthenticationService(mockUnitOfWork.Object, mockCryptService.Object,
                mockAuthenticationTokenService.Object);
            //Act
            var result = service.Authenticate("", "");

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Status);
            Assert.Equal("Usuário não encontrado.", result.Message);
        }

        [Fact]
        public void WhenCryptServiceReturnInvalidPassword_ShouldReturnFalse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(moq => moq.RepositoryBase.FirstOrDefault<User>(
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>()))
                .Returns(new User() { Password = "" });

            var mockCryptService = new Mock<ICryptService>();
            mockCryptService
                .Setup(moq => moq.IsValid(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            var mockAuthenticationTokenService = new Mock<IAuthenticationTokenService>();

            //Act
            var service = new AuthenticationService(mockUnitOfWork.Object, mockCryptService.Object,
                mockAuthenticationTokenService.Object);
            var result = service.Authenticate("", "");

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Status);
            Assert.Equal("Senha inválida!", result.Message);
        }     

        [Fact]
        public void WhenUserIsDisabled_ShouldReturnFalse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(moq => moq.RepositoryBase.FirstOrDefault<User>(
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>()))
                .Returns(new User() { Password = "", Status = false });

            var mockCryptService = new Mock<ICryptService>();
            mockCryptService
                .Setup(moq => moq.IsValid(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            var mockAuthenticationTokenService = new Mock<IAuthenticationTokenService>();

            //Act
            var service = new AuthenticationService(mockUnitOfWork.Object, mockCryptService.Object,
                mockAuthenticationTokenService.Object);
            var result = service.Authenticate("", "");

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Status);
            Assert.Equal("Usuário desativado.", result.Message);
        }

        [Fact]
        public void WhenUserIsValid_ShouldReturnToken()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(moq => moq.RepositoryBase.FirstOrDefault<User>(
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>()))
                .Returns(new User() { Password = "", Status = true });

            var mockCryptService = new Mock<ICryptService>();
            mockCryptService
                .Setup(moq => moq.IsValid(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            var mockAuthenticationTokenService = new Mock<IAuthenticationTokenService>();
            mockAuthenticationTokenService
                .Setup(moq => moq.CreateAuthenticationToken(It.IsAny<User>()))
                .Returns("111111");

            //Act
            var service = new AuthenticationService(mockUnitOfWork.Object, mockCryptService.Object,
                mockAuthenticationTokenService.Object);
            var result = service.Authenticate("", "");

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Status);
            Assert.NotNull(result.Message);
            Assert.Equal(6, result.Message.Length);
        }
    }
}
