namespace Smartwyre.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Smartwyre.Application.Interfaces.Transversal;
    using Smartwyre.Domain.Entities.Dto;
    using Smartwyre.Domain.Entities.Dto.Transversal;

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
