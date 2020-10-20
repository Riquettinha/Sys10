using Microsoft.AspNetCore.Mvc;
using Sys10.Services.Objects;
using Sys10.Services.Services;

namespace Api.Controllers
{
    public class _ControllerBase : Controller
    {
        private readonly IAuthenticationTokenService _authenticationTokenService;
        public _ControllerBase(IAuthenticationTokenService authenticationTokenService)
        {
            _authenticationTokenService = authenticationTokenService;
        }

        public Result ValidateToken()
        {
            var userName = HttpContext.Request.Headers["name"];
            var userToken = HttpContext.Request.Headers["token"];
            var result = _authenticationTokenService.ValidateToken(userName, userToken);

            return result;
        }
    }
}
