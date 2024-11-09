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
    using Smartwyre.Application.Interfaces.Operation;
    using Smartwyre.Domain.Entities.Dto.Operation;

    [Route("api/[controller]")]
    public class RebateController : Controller
    {
        private IRebateApplication rebateApplication;

        public RebateController(IRebateApplication rebateApplication)
        {
            this.rebateApplication = rebateApplication;
        }

        [Authorize]
        [HttpPost]
        [Route("CalculateRebate")]
        public IActionResult CalculateRebate([FromBody] CalculateRebateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string host = HttpContext.Request.Host.Value;
            string token = Request.Headers[MyHeadersEnum.Authorization];
            string userName = HeaderClaims.GetClaimValue(token, MyClaimsEnum.unique_name);
            return Ok(this.rebateApplication.Calculate(request));
        }
    }
}
