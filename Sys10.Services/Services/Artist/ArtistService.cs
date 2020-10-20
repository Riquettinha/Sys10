using Sys10.Data.UnitOfWork;
using System;
using System.Linq;

namespace Sys10.Services.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ArtistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Guid? Get(string name, Data.Enums.Artist.Type type)
        {
            var artist = _unitOfWork.RepositoryBase
                .Get<Data.Models.Artist>(u => u.Name == name && u.Type == type)
                .Select(u => u.Id)
                .FirstOrDefault();

            return artist;
        }
    }
}
