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
    public class Create
    {
        [Fact]
        public void WhenMoovieAlreadyExist_ShouldReturnFalse()
        {
            //Arrange
            var mockCountryService = new Mock<ICountryService>();
            var mockGenreService = new Mock<IGenreService>();
            var mockArtistService = new Mock<IArtistService>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(a => a.RepositoryBase.Get<Moovie>(
              It.IsAny<Expression<Func<Moovie, bool>>>(),
              It.IsAny<Func<IQueryable<Moovie>,
              IOrderedQueryable<Moovie>>>()))
                 .Returns(new List<Moovie>() { new Moovie() { Id = Guid.NewGuid() } }.AsQueryable());

            var service = new MoovieService(mockUnitOfWork.Object, mockArtistService.Object,
                mockCountryService.Object, mockGenreService.Object);

            //Act
            var result = service.Create(new Objects.CreateMoovieModel());

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Status);
            Assert.Equal("Filme já existente.", result.Message);
        }

        [Fact]
        public void WhenDirectorDoesNotExist_ShouldReturnFalse()
        {
            //Arrange
            var mockCountryService = new Mock<ICountryService>();
            var mockGenreService = new Mock<IGenreService>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(a => a.RepositoryBase.Get<Moovie>(
              It.IsAny<Expression<Func<Moovie, bool>>>(),
              It.IsAny<Func<IQueryable<Moovie>,
              IOrderedQueryable<Moovie>>>()))
                 .Returns(new List<Moovie>().AsQueryable());

            var mockArtistService = new Mock<IArtistService>();
            mockArtistService
                .Setup(moq => moq.Get(It.IsAny<string>(), It.IsAny<Data.Enums.Artist.Type>()))
                .Returns<Guid?>(null);

            var service = new MoovieService(mockUnitOfWork.Object, mockArtistService.Object,
                mockCountryService.Object, mockGenreService.Object);

            //Act
            var result = service.Create(new Objects.CreateMoovieModel());

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Status);
            Assert.Equal("Diretor não encontrado.", result.Message);
        }

        [Fact]
        public void WhenCountryDoesNotExist_ShouldReturnFalse()
        {
            //Arrange
            var mockGenreService = new Mock<IGenreService>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(a => a.RepositoryBase.Get<Moovie>(
              It.IsAny<Expression<Func<Moovie, bool>>>(),
              It.IsAny<Func<IQueryable<Moovie>,
              IOrderedQueryable<Moovie>>>()))
                 .Returns(new List<Moovie>().AsQueryable());

            var mockArtistService = new Mock<IArtistService>();
            mockArtistService
                .Setup(moq => moq.Get(It.IsAny<string>(), It.IsAny<Data.Enums.Artist.Type>()))
                .Returns(Guid.NewGuid());

            var mockCountryService = new Mock<ICountryService>();
            mockCountryService
                .Setup(moq => moq.Get(It.IsAny<string>()))
                .Returns<Guid?>(null);

            var service = new MoovieService(mockUnitOfWork.Object, mockArtistService.Object,
                mockCountryService.Object, mockGenreService.Object);

            //Act
            var result = service.Create(new Objects.CreateMoovieModel());

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Status);
            Assert.Equal("Nacionalidade não encontrada.", result.Message);
        }

        [Fact]
        public void WhenGenreDoesNotExist_ShouldReturnFalse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(a => a.RepositoryBase.Get<Moovie>(
              It.IsAny<Expression<Func<Moovie, bool>>>(),
              It.IsAny<Func<IQueryable<Moovie>,
              IOrderedQueryable<Moovie>>>()))
                 .Returns(new List<Moovie>().AsQueryable());

            var mockArtistService = new Mock<IArtistService>();
            mockArtistService
                .Setup(moq => moq.Get(It.IsAny<string>(), It.IsAny<Data.Enums.Artist.Type>()))
                .Returns(Guid.NewGuid());

            var mockCountryService = new Mock<ICountryService>();
            mockCountryService
                .Setup(moq => moq.Get(It.IsAny<string>()))
                .Returns(Guid.NewGuid());

            var mockGenreService = new Mock<IGenreService>();
            mockGenreService
                .Setup(moq => moq.Get(It.IsAny<string>()))
                .Returns<Guid?>(null);

            var service = new MoovieService(mockUnitOfWork.Object, mockArtistService.Object,
                mockCountryService.Object, mockGenreService.Object);

            //Act
            var result = service.Create(new Objects.CreateMoovieModel());

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Status);
            Assert.Equal("Gênero de filme não encontrado.", result.Message);
        }

        [Fact]
        public void WhenExtraDataExist_ShouldReturnTrue()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(a => a.RepositoryBase.Get<Moovie>(
              It.IsAny<Expression<Func<Moovie, bool>>>(),
              It.IsAny<Func<IQueryable<Moovie>,
              IOrderedQueryable<Moovie>>>()))
                 .Returns(new List<Moovie>().AsQueryable());
            mockUnitOfWork
                .Setup(moq => moq.RepositoryBase.Add(It.IsAny<Data.Models.Moovie>()));
            mockUnitOfWork
                .Setup(moq => moq.Commit());

            var mockArtistService = new Mock<IArtistService>();
            mockArtistService
                .Setup(moq => moq.Get(It.IsAny<string>(), It.IsAny<Data.Enums.Artist.Type>()))
                .Returns(Guid.NewGuid());

            var mockCountryService = new Mock<ICountryService>();
            mockCountryService
                .Setup(moq => moq.Get(It.IsAny<string>()))
                .Returns(Guid.NewGuid());

            var mockGenreService = new Mock<IGenreService>();
            mockGenreService
                .Setup(moq => moq.Get(It.IsAny<string>()))
                .Returns(Guid.NewGuid());

            var service = new MoovieService(mockUnitOfWork.Object, mockArtistService.Object,
                mockCountryService.Object, mockGenreService.Object);

            //Act
            var result = service.Create(new Objects.CreateMoovieModel());

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Status);
            Assert.Equal("Filme adicionado com sucesso!", result.Message);
        }

    }
}
