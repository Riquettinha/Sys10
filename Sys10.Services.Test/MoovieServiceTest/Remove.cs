using Moq;
using Sys10.Data.Models;
using Sys10.Data.UnitOfWork;
using Sys10.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Sys10.Services.Test.MoovieServiceTest
{
    public class Remove
    {
        [Fact]
        public void WhenMoovieNameDoesNotExist_ShouldReturnFalse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(a => a.RepositoryBase.Get<Moovie>(
              It.IsAny<Expression<Func<Moovie, bool>>>(),
              It.IsAny<Func<IQueryable<Moovie>,
              IOrderedQueryable<Moovie>>>()))
                 .Returns(new List<Moovie>().AsQueryable());

            var mockCountryService = new Mock<ICountryService>();
            var mockGenreService = new Mock<IGenreService>();
            var mockArtistService = new Mock<IArtistService>();

            var service = new MoovieService(mockUnitOfWork.Object, mockArtistService.Object,
                mockCountryService.Object, mockGenreService.Object);

            //Act
            var result = service.Remove("");

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Status);
            Assert.Equal("Filme não encontrado.", result.Message);
        }

        [Fact]
        public void WhenMoovieIdDoesNotExist_ShouldReturnFalse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(a => a.RepositoryBase.Get<Moovie>(
              It.IsAny<Expression<Func<Moovie, bool>>>(),
              It.IsAny<Func<IQueryable<Moovie>,
              IOrderedQueryable<Moovie>>>()))
                 .Returns(new List<Moovie>().AsQueryable());
            mockUnitOfWork
                .Setup(moq => moq.RepositoryBase.FirstOrDefault<Moovie>(
                    It.IsAny<Expression<Func<Moovie, bool>>>(),
                    It.IsAny<Func<IQueryable<Moovie>, IOrderedQueryable<Moovie>>>()))
                .Returns<Moovie>(null);

            var mockCountryService = new Mock<ICountryService>();
            var mockGenreService = new Mock<IGenreService>();
            var mockArtistService = new Mock<IArtistService>();

            var service = new MoovieService(mockUnitOfWork.Object, mockArtistService.Object,
                mockCountryService.Object, mockGenreService.Object);

            //Act
            var result = service.Remove("");

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Status);
            Assert.Equal("Filme não encontrado.", result.Message);
        }

        [Fact]
        public void WhenMoovieIdExist_ShouldReturnTrue()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(a => a.RepositoryBase.Get<Moovie>(
              It.IsAny<Expression<Func<Moovie, bool>>>(),
              It.IsAny<Func<IQueryable<Moovie>,
              IOrderedQueryable<Moovie>>>()))
                 .Returns(new List<Moovie>().AsQueryable());
            mockUnitOfWork
                .Setup(moq => moq.RepositoryBase.FirstOrDefault<Moovie>(
                    It.IsAny<Expression<Func<Moovie, bool>>>(),
                    It.IsAny<Func<IQueryable<Moovie>, IOrderedQueryable<Moovie>>>()))
                .Returns(new Moovie());
            mockUnitOfWork
                .Setup(moq => moq.RepositoryBase.Delete(It.IsAny<Moovie>()));
            mockUnitOfWork
                .Setup(moq => moq.Commit());

            var mockCountryService = new Mock<ICountryService>();
            var mockGenreService = new Mock<IGenreService>();
            var mockArtistService = new Mock<IArtistService>();

            var service = new MoovieService(mockUnitOfWork.Object, mockArtistService.Object,
                mockCountryService.Object, mockGenreService.Object);

            //Act
            var result = service.Remove("");

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Status);
            Assert.Equal("Filme removido com sucesso!", result.Message);
        }
    }
}
