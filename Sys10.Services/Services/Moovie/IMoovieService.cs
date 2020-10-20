using Sys10.Services.Services.Objects;
using System;

namespace Sys10.Services.Services
{
    public interface IMoovieService
    {
        Result Create(string name, DateTime releaseDate, string directorName, string countryName, string genreName, string observations);
        Result Edit(string name, DateTime releaseDate, string directorName, string countryName, string genreName, string observations);
        Result Edit(Guid id, string name, DateTime releaseDate, string directorName, string countryName, string genreName, string observations);
        Result Remove(Guid id);
        Result Remove(string name);
        Result GetBasicInfo(Guid id);
        Result GetBasicInfo(string name);
    }
}
