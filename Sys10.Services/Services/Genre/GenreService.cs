using Sys10.Data.UnitOfWork;
using System;
using System.Linq;

namespace Sys10.Services.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Guid? Get(string name)
        {
            var genre = _unitOfWork.RepositoryBase
                .Get<Data.Models.Genre>(u => u.Name == name)
                .Select(u => u.Id)
                .FirstOrDefault();

            return genre;
        }
    }
}
