using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sys10.Services.Objects;
using Sys10.Services.Services;
using System;

namespace Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class MoovieController : _ControllerBase
    {
        private readonly IMoovieService _moovieService;

        public MoovieController(IMoovieService moovieService, IAuthenticationTokenService authenticationTokenService)
            : base(authenticationTokenService)
        {
            _moovieService = moovieService;
        }

        [HttpGet, Route("byName/{name}")]
        public IActionResult GetByName(string name)
        {
            var tokenValidationResult = ValidateToken();
            if(!tokenValidationResult.Status)
            {
                var tokenValidationResultJson = JsonConvert.SerializeObject(tokenValidationResult);
                return Content(tokenValidationResultJson, "aplication/json");
            }    

            var result = _moovieService.GetBasicInfo(name);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "aplication/json");
        }

        [HttpGet, Route("byId/{id}")]
        public IActionResult GetById(Guid id)
        {
            var tokenValidationResult = ValidateToken();
            if(!tokenValidationResult.Status)
            {
                var tokenValidationResultJson = JsonConvert.SerializeObject(tokenValidationResult);
                return Content(tokenValidationResultJson, "aplication/json");
            }    

            var result = _moovieService.GetBasicInfo(id);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "aplication/json");
        }

        [HttpPut, Route("create")]
        public IActionResult Create([FromForm]CreateMoovieModel moovie)
        {
            var tokenValidationResult = ValidateToken();
            if (!tokenValidationResult.Status)
            {
                var tokenValidationResultJson = JsonConvert.SerializeObject(tokenValidationResult);
                return Content(tokenValidationResultJson, "aplication/json");
            }

            var result = _moovieService.Create(moovie);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "aplication/json");
        }

        [HttpPut, Route("update")]
        public IActionResult Update([FromForm]EditMoovieModel moovie)
        {
            var tokenValidationResult = ValidateToken();
            if (!tokenValidationResult.Status)
            {
                var tokenValidationResultJson = JsonConvert.SerializeObject(tokenValidationResult);
                return Content(tokenValidationResultJson, "aplication/json");
            }

            var result = _moovieService.Edit(moovie);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "aplication/json");
        }

        [HttpDelete, Route("byName/{name}")]
        public IActionResult DeleteByName(string name)
        {
            var tokenValidationResult = ValidateToken();
            if (!tokenValidationResult.Status)
            {
                var tokenValidationResultJson = JsonConvert.SerializeObject(tokenValidationResult);
                return Content(tokenValidationResultJson, "aplication/json");
            }

            var result = _moovieService.Remove(name);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "aplication/json");
        }

        [HttpDelete, Route("byId/{id}")]
        public IActionResult DeleteById(Guid id)
        {
            var tokenValidationResult = ValidateToken();
            if (!tokenValidationResult.Status)
            {
                var tokenValidationResultJson = JsonConvert.SerializeObject(tokenValidationResult);
                return Content(tokenValidationResultJson, "aplication/json");
            }

            var result = _moovieService.Remove(id);
            var resultJson = JsonConvert.SerializeObject(result);
            return Content(resultJson, "aplication/json");
        }
    }
}
