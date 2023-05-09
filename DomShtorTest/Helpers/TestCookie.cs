using DomShtor.BL.General;

namespace DomShtorTest.Helpers;

public class TestCookie: IWebCoookie
{
    private Dictionary<string, string> cookie = new Dictionary<string, string>();
    public void AddSecure(string cookieName, string value, int days = 0)
    {
        cookie.Add(cookieName, value);
    }

    public void Add(string cookieName, string value, int days = 0)
    {
        cookie.Add(cookieName, value);
    }

    public void Delete(string cookieName)
    {
        cookie.Remove(cookieName);
    }

    public string? Get(string cookieName)
    {
        if (cookie.ContainsKey(cookieName))
            return cookie[cookieName];
        return null;
    }
}