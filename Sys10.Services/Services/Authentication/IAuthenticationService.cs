using Sys10.Services.Services.Objects;

namespace Sys10.Services.Services
{
    public interface IAuthenticationService
    {
        Result Authenticate(string name, string password);
    }
}
