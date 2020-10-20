using Sys10.Data.Models;
using Sys10.Data.UnitOfWork;
using Sys10.Services.Services.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys10.Services.Services
{
    public class MoovieService : IMoovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArtistService _artistService;
        private readonly ICountryService _countryService;
        private readonly IGenreService _genreService;

        public MoovieService(IUnitOfWork unitOfWork, IArtistService artistService,
            ICountryService countryService, IGenreService genreService)
        {
            _unitOfWork = unitOfWork;
            _artistService = artistService;
            _countryService = countryService;
            _genreService = genreService;
        }

        public Result Create(string name, DateTime releaseDate, string directorName, string countryName, string genreName, string observations)
        {
            var directorId = _artistService.Get(directorName, Data.Enums.Artist.Type.Director);
            if (directorId == null)
                return new Result()
                {
                    Status = false,
                    Message = "Diretor não encontrado."
                };

            var countryId = _countryService.Get(countryName);
            if (countryId == null)
                return new Result()
                {
                    Status = false,
                    Message = "Nacionalidade não encontrada."
                };

            var genreId = _genreService.Get(genreName);
            if (genreId == null)
                return new Result()
                {
                    Status = false,
                    Message = "Gênero de filme não encontrado."
                };

            var moovie = new Moovie()
            {
                Id = Guid.NewGuid(),
                Name = name,
                ReleaseDate = releaseDate,
                DirectorId = (Guid)directorId,
                CountryId = (Guid)countryId,
                GenreId = (Guid)genreId,
                Observations = observations
            };
            _unitOfWork.RepositoryBase.Add(moovie);
            _unitOfWork.Commit();

            return new Result()
            {
                Status = true,
                Message = "Filme adicionado com sucesso!"
            };
        }

        public Result Edit(string name, DateTime releaseDate, string directorName, string countryName, string genreName, string observations)
        {
            var moovieId = GetId(name);
            if (moovieId == null)
                return new Result()
                {
                    Status = false,
                    Message = "Filme não encontrado."
                };

            var result = Edit((Guid)moovieId, name, releaseDate, directorName, countryName, genreName, observations);
            return result;
        }

        public Result Edit(Guid id, string name, DateTime releaseDate, string directorName, string countryName, string genreName, string observations)
        {
            var moovie = Get(id);
            if(moovie == null)
                return new Result()
                {
                    Status = false,
                    Message = "Filme não encontrado."
                };

            var directorId = _artistService.Get(directorName, Data.Enums.Artist.Type.Director);
            if (directorId == null)
                return new Result()
                {
                    Status = false,
                    Message = "Diretor não encontrado."
                };

            var countryId = _countryService.Get(countryName);
            if (countryId == null)
                return new Result()
                {
                    Status = false,
                    Message = "Nacionalidade não encontrada."
                };

            var genreId = _genreService.Get(genreName);
            if (genreId == null)
                return new Result()
                {
                    Status = false,
                    Message = "Gênero de filme não encontrado."
                };

            moovie.Name = name;
            moovie.ReleaseDate = releaseDate;
            moovie.DirectorId = (Guid)directorId;
            moovie.CountryId = (Guid)countryId;
            moovie.GenreId = (Guid)genreId;
            moovie.Observations = observations;
            _unitOfWork.RepositoryBase.Edit(moovie);
            _unitOfWork.Commit();

            return new Result()
            {
                Status = true,
                Message = "Filme adicionado com sucesso!"
            };
        }

        public Result Remove(Guid id)
        {
            var moovie = Get(id);
            if (moovie == null)
                return new Result()
                {
                    Status = false,
                    Message = "Filme não encontrado."
                };

            _unitOfWork.RepositoryBase.Delete(moovie);
            _unitOfWork.Commit();

            return new Result()
            {
                Status = true,
                Message = "Filme removido com sucesso!"
            };
        }

        public Result Remove(string name)
        {
            var moovieId = GetId(name);
            if (moovieId == null)
                return new Result()
                {
                    Status = false,
                    Message = "Filme não encontrado."
                };

            var result = Remove((Guid)moovieId);

            return result;
        }

        public Result GetBasicInfo(Guid id)
        {
            var moovie = Get(id);
            if (moovie == null)
                return new Result()
                {
                    Status = false,
                    Message = "Filme não encontrado."
                };

            var basicMoovieInfo = new MoovieBasic()
            {
                Name = moovie.Name,
                ReleaseDate = moovie.ReleaseDate,
                DirectorName = moovie.Director.Name,
                CountryName = moovie.Country.Name,
                GenreName = moovie.Genre.Name,
                Observations = moovie.Observations
            };

            return new Result()
            {
                Status = true,
                Message = "Filme localizado.",
                Object = basicMoovieInfo
            };
        }

        public Result GetBasicInfo(string name)
        {
            var moovieId = GetId(name);
            if (moovieId == null)
                return new Result()
                {
                    Status = false,
                    Message = "Filme não encontrado."
                };

            var result = GetBasicInfo((Guid)moovieId);
            return result;
        }

        private Guid? GetId(string name)
        {
            var moovieId = _unitOfWork.RepositoryBase
                .Get<Moovie>(m => m.Name == name)
                .Select(m => m.Id)
                .FirstOrDefault();

            return moovieId;
        }

        private Moovie Get(Guid id)
        {
            var moovie = _unitOfWork.RepositoryBase
                .FirstOrDefault<Moovie>(m => m.Id == id);

            return moovie;
        }
    }
}
