namespace Smartwyre.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Smartwyre.Application.Interfaces.Transversal;
    using Smartwyre.Domain.Entities.Dto;
    using Smartwyre.WebApi.Middleware;
    using Smartwyre.Domain.Entities.Enums;
    using Smartwyre.Domain.Entities.Response;
    using Common.Utils.Utils.Interface;
    using System.Security.Claims;

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
