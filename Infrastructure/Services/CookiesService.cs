namespace Biokudi_Backend.Infrastructure.Services
{
    public class CookiesService
    {
        public void SetAuthCookies(HttpContext context, string token, string userId, bool rememberMe)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = rememberMe ? DateTime.UtcNow.AddMonths(2) : (DateTime?)null,
                IsEssential = true
            };
            context.Response.Cookies.Append("jwt", token, cookieOptions);

            var userIdOptions = new CookieOptions
            {
                HttpOnly = true, 
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = rememberMe ? DateTime.UtcNow.AddMonths(2) : (DateTime?)null,
                IsEssential = true
            };
            context.Response.Cookies.Append("userId", userId, userIdOptions);
        }

        public void RemoveCookies(HttpContext context)
        {
            context.Response.Cookies.Delete("jwt");
            context.Response.Cookies.Delete("userId");
        }
    }
}
