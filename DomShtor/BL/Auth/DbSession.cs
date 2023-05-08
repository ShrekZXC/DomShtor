using DomShtor.DAL;
using DomShtor.Models;

namespace DomShtor.BL.Auth;

public class DbSession: IDbSession
{
    private IDbSessionDAL _sessionDal;
    private IHttpContextAccessor _httpContextAccessor;

    public DbSession(IDbSessionDAL sessionDal, IHttpContextAccessor httpContextAccessor)
    {
        _sessionDal = sessionDal;
        _httpContextAccessor = httpContextAccessor;
    }
    private void CreateSessionCookie(Guid sessionId)
    {
        CookieOptions options = new CookieOptions();
        options.Path = "/";
        options.HttpOnly = true;
        options.Secure = true;
        _httpContextAccessor?.HttpContext?.Response.Cookies.Delete(AuthConstants.SessionCookieName);
        _httpContextAccessor?.HttpContext?.Response.Cookies.Append(AuthConstants.SessionCookieName, sessionId.ToString(), options);
    }

    private async Task<SessionModel> CreateSession()
    {
        var data = new SessionModel()
        {
            DbSessionId = Guid.NewGuid(),
            Created = DateTime.Now,
            LastAccessed = DateTime.Now
        };
        await _sessionDal.Create(data);
        return data;
    }

    private SessionModel? sessionModel = null;

    public async Task<SessionModel> GetSession()
    {
        if (sessionModel != null)
            return sessionModel;
        Guid sessionId;
        var cookie = _httpContextAccessor?.HttpContext?.Request?.Cookies.FirstOrDefault(m=>m.Key == AuthConstants.SessionCookieName);
        if (cookie != null && cookie.Value.Value != null)
            sessionId = Guid.Parse(cookie.Value.Value);
        else
            sessionId = Guid.NewGuid();
        
        var data = await this._sessionDal.Get(sessionId);
        if (data == null)
        {
            data = await this.CreateSession();
            CreateSessionCookie(data.DbSessionId);
        }

        sessionModel = data;
        return data;
    }

    public async Task<int> SetUserId(int userId)
    {
        var data = await this.GetSession();

        data.UserId = userId;
        data.DbSessionId = Guid.NewGuid();
        CreateSessionCookie(data.DbSessionId);
        return await _sessionDal.Create(data);
    }

    public async Task<int?> GetUserId()
    {
        var data = await this.GetSession();
        return data.UserId;
    }

    public async Task<bool> IsLoggedIn()
    {
        var data = await this.GetSession();
        return data.UserId != null;
    }

    public async Task Lock()
    {
        var data = await this.GetSession();
        await _sessionDal.Lock(data.DbSessionId);
    }
}