using Moq;
using Sys10.Data.Models;
using Sys10.Data.UnitOfWork;
using Sys10.Services.Services;
using Xunit;

namespace Sys10.Services.Test.AuthenticationTokenServiceTest
{
    public class CreateAuthenticationToken
    {
        [Fact]
        public void WhenUserIsValid_ShouldAddToken()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(moq => moq.RepositoryBase.Edit(It.IsAny<User>()));
            mockUnitOfWork.Setup(moq => moq.Commit());

            var service = new AuthenticationTokenService(mockUnitOfWork.Object);

            //Act
            var result = service.CreateAuthenticationToken(new User());

            //Assert
            mockUnitOfWork.Verify(a => a.RepositoryBase.Edit(It.IsAny<User>()), Times.Once);
            mockUnitOfWork.Verify(a => a.Commit(), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(6, result.Length);
        }
    }
}
