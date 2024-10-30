using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Biokudi_Backend.Infrastructure.Config
{
    public static class AuthConfig
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtKey = configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey)) throw new ArgumentNullException(nameof(jwtKey), "JWT key cannot be null or empty.");
            var key = Encoding.ASCII.GetBytes(jwtKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.FromSeconds(30)
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var endpoint = context.HttpContext.GetEndpoint();
                        if (endpoint != null)
                        {
                            var authorizeAttribute = endpoint.Metadata.GetMetadata<AuthorizeAttribute>();
                            if (authorizeAttribute == null)
                            {
                                return Task.CompletedTask;
                            }
                        }
                        var token = context.Request.Cookies["jwt"];
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = async context =>
                    {
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            var cookiesService = context.HttpContext.RequestServices.GetRequiredService<CookiesService>();
                            var authService = context.HttpContext.RequestServices.GetRequiredService<AuthService>();

                            var refreshToken = cookiesService.GetRefreshToken(context.HttpContext);

                            if (!string.IsNullOrEmpty(refreshToken))
                            {
                                var principal = authService.ValidateRefreshToken(refreshToken);
                                if (principal != null)
                                {
                                    var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                                    var role = principal.FindFirst(ClaimTypes.Role)?.Value;

                                    if (userId != null)
                                    {
                                        var (newJwtToken, newRefreshToken) = authService.GenerateTokens(userId, role);

                                        cookiesService.RenewAuthCookies(context.HttpContext, newJwtToken, newRefreshToken);

                                        context.HttpContext.Request.Headers["Authorization"] = $"Bearer {newJwtToken}";

                                        context.Principal = principal;
                                        context.Success();
                                        return;
                                    }
                                }
                            }

                            context.Response.StatusCode = 401;
                            await context.Response.WriteAsync("Token expired and refresh failed.");
                        }
                    },
                    OnTokenValidated = context =>
                    {
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}