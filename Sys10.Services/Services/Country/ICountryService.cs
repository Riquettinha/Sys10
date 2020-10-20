using System;

namespace Sys10.Services.Services
{
    public interface ICountryService
    {
        Guid? Get(string name);
    }
}

