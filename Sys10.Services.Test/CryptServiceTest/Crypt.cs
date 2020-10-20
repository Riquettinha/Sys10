using Sys10.Services.Services;
using Xunit;

namespace Sys10.Services.Test.CryptServiceTest
{
    public class Crypt
    {
        private const string Password = "123456";
        private const string PasswordHash = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92";

        [Fact]
        public void WhenIsTestPassword_ShouldCryptPassword()
        {
            //Arrange
            var service = new CryptService();

            //Act
            var result = service.Crypt(Password);

            //Assert
            Assert.Equal(PasswordHash, result);
        }
    }
}
