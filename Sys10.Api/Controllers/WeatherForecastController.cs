using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sys10.Data.Models;
using Sys10.Data.UnitOfWork;

namespace Sys10.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public WeatherForecastController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IEnumerable<User> Get()
        {
            var aux = _unitOfWork.RepositoryBase.Get<User>();

            return aux;
        }
    }
}
