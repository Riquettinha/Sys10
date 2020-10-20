using System;

namespace Sys10.Services.Services
{
    public interface IGenreService
    {
        Guid? Get(string name);
    }
}
