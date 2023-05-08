namespace DomShtor.BL.General;

public interface IWebCoookie
{
    public void AddSecure(string cookieName, string value);

    void Add(string cookieName, string value);

    void Delete(string cookieName);

    string? Get(string cookieName);
}