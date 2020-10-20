using Moq;
using Sys10.Data.Helper;
using Sys10.Data.Models;
using Sys10.Data.UnitOfWork;
using Sys10.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sys10.Services.Test.MoovieServiceTest
{
    public class GetBasicInfo
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
            var result = service.GetBasicInfo("");

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
            var result = service.GetBasicInfo("");

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Status);
            Assert.Equal("Filme não encontrado.", result.Message);
        }

        [Fact]
        public void WhenMoovieIdExist_ShouldReturnTrue()
        {
            //Arrange
            var moovie = new Moovie()
            {
                Name = "",
                ReleaseDate = DateTime.Now.Brasilia(),
                Director = new Artist() { Name = "" },
                Country = new Country() { Name = "" },
                Genre = new Genre() { Name = "" },
                Observations = ""
            };

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
                .Returns(moovie);

            var mockCountryService = new Mock<ICountryService>();
            var mockGenreService = new Mock<IGenreService>();
            var mockArtistService = new Mock<IArtistService>();

            var service = new MoovieService(mockUnitOfWork.Object, mockArtistService.Object,
                mockCountryService.Object, mockGenreService.Object);

            //Act
            var result = service.GetBasicInfo("");

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Status);
            Assert.Equal("Filme localizado.", result.Message);
            Assert.NotNull(result.Object);
        }
    }
}
