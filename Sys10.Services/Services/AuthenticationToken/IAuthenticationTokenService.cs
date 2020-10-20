using Sys10.Data.Models;

namespace Sys10.Services.Services
{
    public interface IAuthenticationTokenService
    {
        string CreateAuthenticationToken(User user);
    }
}
