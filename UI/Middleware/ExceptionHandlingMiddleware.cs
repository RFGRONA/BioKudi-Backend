namespace Biokudi_Backend.UI.Middleware
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Biokudi_Backend.Domain.Exceptions;

    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); 
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            string? result;

            switch (exception)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;  // 404 Not Found
                    result = JsonConvert.SerializeObject(new { error = notFoundException.Message });
                    break;

                case BusinessRuleViolationException businessException:
                    statusCode = HttpStatusCode.BadRequest;  // 400 Bad Request
                    result = JsonConvert.SerializeObject(new { error = businessException.Message });
                    break;

                case DatabaseUpdateException dbException:
                    statusCode = HttpStatusCode.InternalServerError;  // 500 Internal Server Error
                    result = JsonConvert.SerializeObject(new { error = dbException.Message });
                    break;

                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;  // 400 Bad Request
                    result = JsonConvert.SerializeObject(new { error = validationException.Message });
                    break;

                default:
                    result = JsonConvert.SerializeObject(new { error = "Ha ocurrido un error en el servidor." });
                    break;
            }

            context.Response.ContentType = "application/json";  
            context.Response.StatusCode = (int)statusCode;  

            return context.Response.WriteAsync(result); 
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
