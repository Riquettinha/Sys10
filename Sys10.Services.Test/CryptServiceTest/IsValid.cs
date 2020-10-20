using Sys10.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sys10.Services.Test.CryptServiceTest
{
    public class IsValid
    {
        private const string Password = "123456";
        private const string PasswordHash = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92";

        [Fact]
        public void WhenIsTestPassword_ShouldReturnTrue()
        {
            //Arrange
            var service = new CryptService();

            //Act
            var result = service.IsValid(Password, PasswordHash);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void WhenIsDifferenctPassword_ShouldReturnFalse()
        {
            //Arrange
            var service = new CryptService();

            //Act
            var result = service.IsValid("", PasswordHash);

            //Assert
            Assert.False(result);
        }
    }
}
