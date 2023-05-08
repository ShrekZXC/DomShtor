using DomShtor.BL.Auth;

namespace DomShtor.BL.General;

public class WebCookie: IWebCoookie
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WebCookie(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public void AddSecure(string cookieName, string value)
    {
        CookieOptions options = new CookieOptions();
        options.Path = "/";
        options.HttpOnly = true;
        options.Secure = true;
        _httpContextAccessor?.HttpContext?.Response.Cookies.Append(cookieName, value, options);
    }

    public void Add(string cookieName, string value)
    {
        CookieOptions options = new CookieOptions();

        options.Path = "/";
        
        _httpContextAccessor?.HttpContext?.Response.Cookies.Append(cookieName, value, options);
    }

    public void Delete(string cookieName)
    {
        _httpContextAccessor?.HttpContext?.Response.Cookies.Delete(cookieName);
    }

    public string? Get(string cookieName)
    {
        var cookie = _httpContextAccessor?.HttpContext?.Request?.Cookies.FirstOrDefault(m => m.Key == cookieName);
        if (cookie != null && cookie.Value.Value != null)
            return cookie.Value.Value;
        return null;
    }
}