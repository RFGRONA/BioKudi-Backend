namespace Biokudi_Backend.Infrastructure.Config
{
    using Microsoft.AspNetCore.RateLimiting;
    using System.Threading.RateLimiting;

    namespace Biokudi_Backend.Infrastructure.Config
    {
        public static class RateLimitConfig
        {
            public static void ConfigureRateLimiting(IServiceCollection services)
            {
                services.AddRateLimiter(options =>
                {
                    options.AddFixedWindowLimiter("fixed", limiterOptions =>
                    {
                        limiterOptions.PermitLimit = 250; 
                        limiterOptions.Window = TimeSpan.FromMinutes(1); 
                        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; 
                        limiterOptions.QueueLimit = 2;
                    });

                    options.OnRejected = async (context, cancellationToken) =>
                    {
                        var httpContext = context.HttpContext;
                        var cookieService = httpContext.RequestServices.GetService<CookiesService>();
                        cookieService?.RemoveCookies(httpContext);

                        context.HttpContext.Response.StatusCode = 429;
                        await context.HttpContext.Response.WriteAsync("Demasiadas solicitudes. Intenta de nuevo más tarde.", cancellationToken);
                    };
                });
            }
        }
    }

}
