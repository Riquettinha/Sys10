using Sys10.Data.Models;
using Sys10.Data.UnitOfWork;
using Sys10.Services.Objects;
using System;
using System.Linq;

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

        public Result Create(CreateMoovieModel moovie)
        {
            var moovieId = GetId(moovie.Name);
            if (moovieId != null && moovieId != Guid.Empty)
                return new Result()
                {
                    Status = false,
                    Message = "Filme já existente."
                };

            var directorId = _artistService.Get(moovie.DirectorName, Data.Enums.Artist.Type.Director);
            if (directorId == null || directorId == Guid.Empty)
                return new Result()
                {
                    Status = false,
                    Message = "Diretor não encontrado."
                };

            var countryId = _countryService.Get(moovie.CountryName);
            if (countryId  == null || countryId  == Guid.Empty)
                return new Result()
                {
                    Status = false,
                    Message = "Nacionalidade não encontrada."
                };

            var genreId = _genreService.Get(moovie.GenreName);
            if (genreId  == null || genreId == Guid.Empty)
                return new Result()
                {
                    Status = false,
                    Message = "Gênero de filme não encontrado."
                };

            var newMoovieDatabaseModel = new Moovie()
            {
                Id = Guid.NewGuid(),
                Name = moovie.Name,
                ReleaseDate = moovie.ReleaseDate,
                DirectorId = (Guid)directorId,
                CountryId = (Guid)countryId,
                GenreId = (Guid)genreId,
                Observations = moovie.Observations
            };
            _unitOfWork.RepositoryBase.Add(newMoovieDatabaseModel);
            _unitOfWork.Commit();

            return new Result()
            {
                Status = true,
                Message = "Filme adicionado com sucesso!",
                Object = moovie
            };
        }

        public Result Edit(EditMoovieModel moovie)
        {
            if (moovie.Id == null || moovie.Id == Guid.Empty)
                moovie.Id = GetId(moovie.Name);

            if (moovie.Id == null || moovie.Id == Guid.Empty)
                return new Result()
                {
                    Status = false,
                    Message = "Filme não encontrado."
                };

            var currentDatabaseMoovie = Get((Guid)moovie.Id);
            if(currentDatabaseMoovie == null)
                return new Result()
                {
                    Status = false,
                    Message = "Filme não encontrado."
                };

            var directorId = _artistService.Get(moovie.DirectorName, Data.Enums.Artist.Type.Director);
            if (directorId == null || directorId == Guid.Empty)
                return new Result()
                {
                    Status = false,
                    Message = "Diretor não encontrado."
                };

            var countryId = _countryService.Get(moovie.CountryName);
            if (countryId  == null || countryId  == Guid.Empty)
                return new Result()
                {
                    Status = false,
                    Message = "Nacionalidade não encontrada."
                };

            var genreId = _genreService.Get(moovie.GenreName);
            if (genreId  == null || genreId == Guid.Empty)
                return new Result()
                {
                    Status = false,
                    Message = "Gênero de filme não encontrado."
                };

            currentDatabaseMoovie.Name = moovie.Name;
            currentDatabaseMoovie.ReleaseDate = moovie.ReleaseDate;
            currentDatabaseMoovie.DirectorId = (Guid)directorId;
            currentDatabaseMoovie.CountryId = (Guid)countryId;
            currentDatabaseMoovie.GenreId = (Guid)genreId;
            currentDatabaseMoovie.Observations = moovie.Observations;
            _unitOfWork.RepositoryBase.Edit(currentDatabaseMoovie);
            _unitOfWork.Commit();

            return new Result()
            {
                Status = true,
                Message = "Filme editado com sucesso!",
                Object = moovie
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
                Message = "Filme removido com sucesso!",
                Object = moovie.Name
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

            var basicMoovieInfo = new MoovieBasicInfo()
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
            if (moovieId == null || moovieId == Guid.Empty)
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
