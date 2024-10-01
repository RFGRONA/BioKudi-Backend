namespace Biokudi_Backend.UI.Middleware
{
    public class TokenRenewalMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TokenRenewalMiddleware> _logger;

        public TokenRenewalMiddleware(RequestDelegate next, ILogger<TokenRenewalMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, AuthService authService, CookiesService cookiesService)
        {
            await _next(context);

            if (context.Response.Headers.ContainsKey("Authorization"))
            {
                var newToken = context.Response.Headers["Authorization"].ToString().Split(" ").Last();
                var refreshToken = cookiesService.GetRefreshToken(context);

                if (!string.IsNullOrEmpty(refreshToken) && !string.IsNullOrEmpty(newToken))
                {
                    cookiesService.RenewAuthCookies(context, newToken, refreshToken);
                    _logger.LogInformation("Cookies renewed: JWT and RefreshToken");
                }
                else
                {
                    _logger.LogWarning("Failed to renew cookies: RefreshToken or new JWT is empty");
                }
            }
        }
    }

    public static class TokenRenewalMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenRenewal(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenRenewalMiddleware>();
        }
    }
}
