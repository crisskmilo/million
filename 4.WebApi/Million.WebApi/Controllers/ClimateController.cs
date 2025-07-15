using Microsoft.AspNetCore.Mvc;
using Million.Application.Interfaces.Transversal;
using Million.Domain.Entities.Dto;
using Million.WebApi.Middleware;
using Million.Domain.Entities.Enums;
using Million.Domain.Entities.Response;
using Common.Utils.Utils.Interface;
using System.Security.Claims;
using Million.Application.Interfaces.Operation;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Entities.Request;
using System.Threading.Tasks;

namespace Million.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ClimateController : Controller
    {
        private IClimateApplication climateApplication;

        public ClimateController(IClimateApplication climateApplication)
        {
            this.climateApplication = climateApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetCurrentWeather/{location}")]
        public async Task<IActionResult> GetCurrentWeather(string location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.climateApplication.GetCurrentWeatherAsync(location));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("Get5DayForecast/{location}")]
        public async Task<IActionResult> Get5DayForecast(string location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.climateApplication.Get5DayForecastAsync(location));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("SendWeatherEmails/{location}")]
        public async Task<IActionResult> SendWeatherEmails(string location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.climateApplication.SendWeatherEmailToCsvRecipientsAsync(location));
        }
    }
}
