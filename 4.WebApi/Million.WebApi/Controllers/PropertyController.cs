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
using Million.Domain.Entities.Dto.Transversal;
using System.Threading.Tasks;

namespace Million.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PropertyController : Controller
    {
        private IPropertyApplication propertyApplication;

        public PropertyController(IPropertyApplication propertyApplication)
        {
            this.propertyApplication = propertyApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetProperties")]
        public async Task<IActionResult> GetProperties()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyApplication.GetProperties());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetPropertyById/{Id}")]
        public async Task<IActionResult> GetPropertyById(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyApplication.GetPropertyById(Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("AddProperty")]
        public async Task<IActionResult> AddProperty([FromBody] Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyApplication.AddProperty(property));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("UpdateProperty")]
        public async Task<IActionResult> UpdateProperty([FromBody] Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyApplication.UpdateProperty(property));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("DeleteProperty/{Id}")]
        public async Task<IActionResult> DeleteProperty(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyApplication.DeleteProperty(Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPatch]
        [Route("ChangePrice")]
        public async Task<IActionResult> ChangePrice([FromBody] PropertyPriceRequestDto propertyPriceRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyApplication.ChangePrice(propertyPriceRequestDto));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("Search")]
        public async Task<IActionResult> Search([FromBody] SearchDto searchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(await this.propertyApplication.Search(searchDto));
        }
    }
}
