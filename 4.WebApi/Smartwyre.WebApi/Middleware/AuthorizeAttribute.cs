namespace Smartwyre.WebApi.Middleware
{
    using Smartwyre.Domain.Entities.Enums;
    using Smartwyre.Domain.Entities.Model.Authentication;
    using Smartwyre.Domain.Entities.Response;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items[MyHeadersEnum.UserName];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new GeneralResponse (){ isSuccess = false, result = StatusCodes.Status401Unauthorized,  message = AuthorizationCode.UnAuthorized }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
