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
                Domain = ".biokudi.site", 
                Path = "/"
            };
            context.Response.Cookies.Append("jwt", token, cookieOptions);
        }

        public void RemoveCookies(HttpContext context)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,           
                Secure = true,             
                SameSite = SameSiteMode.None, 
                Domain = ".biokudi.site",  
                Path = "/",                
                Expires = DateTime.UtcNow.AddYears(-1)
            };

            context.Response.Cookies.Append("jwt", string.Empty, cookieOptions); 
            context.Response.Cookies.Delete("jwt", cookieOptions); 
        }
    }
}
