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
    public class PropertyTraceController : Controller
    {
        private IPropertyTraceApplication propertyTraceApplication;

        public PropertyTraceController(IPropertyTraceApplication propertyTraceApplication)
        {
            this.propertyTraceApplication = propertyTraceApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetPropertyTraces")]
        public async Task<IActionResult> GetPropertyTraces()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyTraceApplication.GetPropertyTraces());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetPropertyTraceById/{Id}")]
        public async Task<IActionResult> GetPropertyTraceById(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyTraceApplication.GetPropertyTraceById(Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyTrace"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("AddPropertyTrace")]
        public async Task<IActionResult> AddPropertyTrace([FromBody] PropertyTrace propertyTrace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyTraceApplication.AddPropertyTrace(propertyTrace));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyTrace"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("UpdatePropertyTrace")]
        public async Task<IActionResult> UpdatePropertyTrace([FromBody] PropertyTrace propertyTrace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyTraceApplication.UpdatePropertyTrace(propertyTrace));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyTrace"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("DeletePropertyTrace/{Id}")]
        public async Task<IActionResult> DeletePropertyTrace(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyTraceApplication.DeletePropertyTrace(Id));
        }
    }
}
