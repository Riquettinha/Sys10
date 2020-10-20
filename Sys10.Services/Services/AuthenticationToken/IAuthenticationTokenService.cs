using Sys10.Data.Models;
using Sys10.Services.Objects;

namespace Sys10.Services.Services
{
    public interface IAuthenticationTokenService
    {
        string CreateAuthenticationToken(User user);
        Result ValidateToken(string name, string token);
    }
}
