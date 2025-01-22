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
    public class PropertyImageController : Controller
    {
        private IPropertyImageApplication propertyImageApplication;

        public PropertyImageController(IPropertyImageApplication propertyImageApplication)
        {
            this.propertyImageApplication = propertyImageApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetPropertyImages")]
        public async Task<IActionResult> GetPropertyImages()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyImageApplication.GetPropertyImages());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetPropertyImageById/{Id}")]
        public async Task<IActionResult> GetPropertyImageById(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyImageApplication.GetPropertyImageById(Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyImage"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("AddPropertyImage")]
        public async Task<IActionResult> AddPropertyImage([FromBody] PropertyImageRequestDto propertyImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyImageApplication.AddPropertyImage(propertyImage));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyImage"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("UpdatePropertyImage")]
        public async Task<IActionResult> UpdatePropertyImage([FromBody] PropertyImage propertyImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyImageApplication.UpdatePropertyImage(propertyImage));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyImage"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("DeletePropertyImage/{Id}")]
        public async Task<IActionResult> DeletePropertyImage(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyImageApplication.DeletePropertyImage(Id));
        }
    }
}
