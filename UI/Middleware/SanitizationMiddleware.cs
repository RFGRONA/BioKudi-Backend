using Biokudi_Backend.Infrastructure.Services;

namespace Biokudi_Backend.UI.Middleware
{
    public class SanitizationMiddleware(RequestDelegate next, SanitizerService sanitizer)
    {
        private readonly RequestDelegate _next = next;
        private readonly SanitizerService _sanitizer = sanitizer;

        public async Task InvokeAsync(HttpContext context)
        {
            var queryCollection = context.Request.Query.ToDictionary(
                kvp => kvp.Key,
                kvp => _sanitizer.Sanitize(kvp.Value.ToString() ?? string.Empty)
            ).ToDictionary(kvp => kvp.Key, kvp => (string?)kvp.Value);

            context.Request.QueryString = new QueryString(QueryString.Create(queryCollection).ToString());

            foreach (var header in context.Request.Headers.Keys)
            {
                var headerValue = context.Request.Headers[header];
                context.Request.Headers[header] = _sanitizer.Sanitize(headerValue.ToString() ?? string.Empty);
            }

            await _next(context);
        }
    }

    public static class SanitizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseSanitization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SanitizationMiddleware>();
        }
    }
}