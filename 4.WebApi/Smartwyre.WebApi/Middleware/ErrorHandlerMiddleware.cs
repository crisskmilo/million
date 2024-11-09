namespace Smartwyre.WebApi.Middleware
{
    using Smartwyre.Domain.Entities;
    using Smartwyre.Domain.Entities.Config;
    using Smartwyre.Domain.Services.Utilities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Smartwyre.Domain.Entities.Response;
    using Smartwyre.Domain.Entities.ErrorHandler;

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError($"-- Error: {ex.Message}  --- Stack Trace : {ex.StackTrace}");
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = 500;
                ErrorResponse errorResponse = new ErrorResponse
                {
                    ResultCode = Constants.INTERNAL_SERVER_ERROR,
                    ResultMsg = Constants.INTERNAL_SERVER_ERROR_DESC
                };
                GeneralResponse genericResponse = Helper.ManageResponse(errorResponse, false);
                var result = JsonSerializer.Serialize(genericResponse);
                await response.WriteAsync(result);
            }
        }
    }
}