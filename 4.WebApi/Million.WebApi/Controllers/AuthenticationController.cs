using Microsoft.AspNetCore.Mvc;
using Million.Application.Interfaces.Transversal;
using Million.Domain.Entities.Dto;
using Million.Domain.Entities.Dto.Transversal;

namespace Million.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private IAuthenticationApplication _authenticationApplication;

        public AuthenticationController(IAuthenticationApplication authenticationApplication)
        {
            this._authenticationApplication = authenticationApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] CredentialDto credential)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(this._authenticationApplication.Authentication(credential));
        }
    }
}
