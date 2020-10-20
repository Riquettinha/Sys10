using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sys10.Services.Services;

namespace Sys10.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet, Route("{name}/{password}")]
        public IActionResult Authenticate(string name, string password)
        {
            var result = _authenticationService.Authenticate(name, password);
            var resultJson = JsonConvert.SerializeObject(result);

            return Content(resultJson, "application/json");
        }
    }
}
