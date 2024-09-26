namespace Biokudi_Backend.Infrastructure.Services
{
    public class CookiesService
    {
        public void SetAuthCookies(HttpContext context, string token, bool rememberMe)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = rememberMe ? DateTime.UtcNow.AddMonths(2) : (DateTime?)null,
                IsEssential = true,
                //Domain = ".biokudi.site", TODO
                Path = "/"
            };
            context.Response.Cookies.Append("jwt", token, cookieOptions);
        }

        public void RemoveCookies(HttpContext context)
        {
            context.Response.Cookies.Delete("jwt");
        }
    }
}
