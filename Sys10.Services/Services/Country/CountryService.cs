using Sys10.Data.UnitOfWork;
using System;
using System.Linq;

namespace Sys10.Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Guid? Get(string name)
        {
            var genre = _unitOfWork.RepositoryBase
                .Get<Data.Models.Country>(u => u.Name == name)
                .Select(u => u.Id)
                .FirstOrDefault();

            return genre;
        }
    }
}
