using Microsoft.AspNetCore.Mvc;
using Million.Application.Interfaces.Transversal;
using Million.Domain.Entities.Dto;
using Million.WebApi.Middleware;
using Million.Domain.Entities.Enums;
using Million.Domain.Entities.Response;
using Common.Utils.Utils.Interface;
using System.Security.Claims;

namespace Million.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserApplication userApplication;

        public UserController(IUserApplication userApplication)
        {
            this.userApplication = userApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(this.userApplication.GetUsers());
        }
    }
}
