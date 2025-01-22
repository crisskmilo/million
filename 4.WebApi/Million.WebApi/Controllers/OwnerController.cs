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
using System.Threading.Tasks;

namespace Million.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class OwnerController : Controller
    {
        private IOwnerApplication ownerApplication;

        public OwnerController(IOwnerApplication ownerApplication)
        {
            this.ownerApplication = ownerApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetOwners")]
        public async Task<IActionResult> GetOwners()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.ownerApplication.GetOwners());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetOwnerById/{Id}")]
        public async Task<IActionResult> GetOwnerById(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.ownerApplication.GetOwnerById(Id));
        }   

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("AddOwner")]
        public async Task<IActionResult> AddOwner([FromBody] Owner owner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.ownerApplication.AddOwner(owner));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("UpdateOwner")]
        public async Task<IActionResult> UpdateOwner([FromBody] Owner owner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.ownerApplication.UpdateOwner(owner));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("DeleteOwner/{Id}")]
        public async Task<IActionResult> DeleteOwner(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.ownerApplication.DeleteOwner(Id));
        }
    }
}
