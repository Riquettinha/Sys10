using Moq;
using Sys10.Data.Helper;
using Sys10.Data.UnitOfWork;
using Sys10.Services.Services;
using System;
using Xunit;

namespace Sys10.Services.Test.MoovieServiceTest
{
    public class Create
    {
        [Fact]
        public void WhenDirectorDoesNotExist_ShouldReturnFalse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCountryService = new Mock<ICountryService>();
            var mockGenreService = new Mock<IGenreService>();

            var mockArtistService = new Mock<IArtistService>();
            mockArtistService
                .Setup(moq => moq.Get(It.IsAny<string>(), It.IsAny<Data.Enums.Artist.Type>()))
                .Returns<Guid?>(null);

            var service = new MoovieService(mockUnitOfWork.Object, mockArtistService.Object,
                mockCountryService.Object, mockGenreService.Object);

            //Act
            var result = service.Create("", DateTime.Now.Brasilia(), "", "", "", "");

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Status);
            Assert.Equal("Diretor não encontrado.", result.Message);
        }

        [Fact]
        public void WhenCountryDoesNotExist_ShouldReturnFalse()
        {
            //Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGenreService = new Mock<IGenreService>();

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
            var result = service.Create("", DateTime.Now.Brasilia(), "", "", "", "");

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
            var result = service.Create("", DateTime.Now.Brasilia(), "", "", "", "");

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
            var result = service.Create("", DateTime.Now.Brasilia(), "", "", "", "");

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Status);
            Assert.Equal("Filme adicionado com sucesso!", result.Message);
        }

    }
}
