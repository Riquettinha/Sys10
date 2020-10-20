using Sys10.Services.Objects;
using System;

namespace Sys10.Services.Services
{
    public interface IMoovieService
    {
        Result Create(CreateMoovieModel moovie);
        Result Edit(EditMoovieModel moovie);
        Result Remove(Guid id);
        Result Remove(string name);
        Result GetBasicInfo(Guid id);
        Result GetBasicInfo(string name);
    }
}
