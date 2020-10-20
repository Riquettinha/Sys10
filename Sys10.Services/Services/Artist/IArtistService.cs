using System;

namespace Sys10.Services.Services
{
    public interface IArtistService
    {
        Guid? Get(string name, Data.Enums.Artist.Type type);
    }
}
