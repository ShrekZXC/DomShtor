using DomShtor.BL.General;
using DomShtor.DAL;
using DomShtor.Models;

namespace DomShtor.BL.Auth;

public class DbSession: IDbSession
{
    private readonly IDbSessionDAL _sessionDal;
    private readonly IWebCoookie _webCoookie;

    public DbSession(IDbSessionDAL sessionDal, IWebCoookie webCoookie)
    {
        _sessionDal = sessionDal;
        _webCoookie = webCoookie;
    }
    private void CreateSessionCookie(Guid sessionId)
    {
        _webCoookie.Delete(AuthConstants.SessionCookieName);
        _webCoookie.AddSecure(AuthConstants.SessionCookieName, sessionId.ToString());
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
        var sessionString = _webCoookie.Get(AuthConstants.SessionCookieName);
        if (sessionString != null)
            sessionId = Guid.Parse(sessionString);
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