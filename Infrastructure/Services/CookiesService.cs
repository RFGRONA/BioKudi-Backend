public class CookiesService
{
    private readonly string? DOMAIN;

    public CookiesService(IConfiguration configuration)
    {
        IConfiguration _configuration = configuration;
        DOMAIN = _configuration["Cookies:Domain"];
    }

    public void SetAuthCookies(HttpContext context, string jwtToken, string refreshToken, bool stayLoggedIn)
    {
        var jwtCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            IsEssential = true,
            Domain = DOMAIN,
            Path = "/"
        };

        var refreshTokenOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            IsEssential = true,
            Domain = DOMAIN,
            Path = "/"
        };

        var stayLoggedInOption = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            IsEssential = true,
            Domain = DOMAIN,
            Path = "/"
        };

        if (stayLoggedIn)
        {
            jwtCookieOptions.Expires = DateTime.UtcNow.AddHours(1);  
            refreshTokenOptions.Expires = DateTime.UtcNow.AddDays(30);
            stayLoggedInOption.Expires = DateTime.UtcNow.AddDays(30);
        }

        context.Response.Cookies.Append("jwt", jwtToken, jwtCookieOptions);
        context.Response.Cookies.Append("refreshToken", refreshToken, refreshTokenOptions);
        context.Response.Cookies.Append("stayLoggedIn", stayLoggedIn.ToString(), stayLoggedInOption);
    }

    public void RenewAuthCookies(HttpContext context, string jwtToken, string refreshToken)
    {
        var stayLoggedIn = GetStayLoggedIn(context);

        SetAuthCookies(context, jwtToken, refreshToken, stayLoggedIn);
    }

    public bool GetStayLoggedIn(HttpContext context)
    {
        context.Request.Cookies.TryGetValue("stayLoggedIn", out var stayLoggedIn);
        return bool.TryParse(stayLoggedIn, out var result) && result;
    }

    public string? GetRefreshToken(HttpContext context)
    {
        context.Request.Cookies.TryGetValue("refreshToken", out var refreshToken);
        return refreshToken;
    }


    public void RemoveCookies(HttpContext context)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Domain = DOMAIN,
            Path = "/",
            Expires = DateTime.UtcNow.AddYears(-1)
        };

        context.Response.Cookies.Append("jwt", string.Empty, cookieOptions);
        context.Response.Cookies.Append("refreshToken", string.Empty, cookieOptions);
        context.Response.Cookies.Append("stayLoggedIn", string.Empty, cookieOptions);

        context.Response.Cookies.Delete("jwt", cookieOptions);
        context.Response.Cookies.Delete("refreshToken", cookieOptions);
        context.Response.Cookies.Delete("stayLoggedIn", cookieOptions);
    }
}
