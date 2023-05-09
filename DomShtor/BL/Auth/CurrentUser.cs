using DomShtor.BL.General;
using DomShtor.DAL;

namespace DomShtor.BL.Auth;

public class CurrentUser: ICurrentUser
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDbSession _dbSession;
    private readonly IWebCoookie _webCoookie;
    private readonly IUserTokenDAL _userTokenDal;
    
    public CurrentUser(IHttpContextAccessor httpContextAccessor,
        IDbSession dbSession,
        IWebCoookie webCoookie,
        IUserTokenDAL userTokenDal)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbSession = dbSession;
        _webCoookie = webCoookie;
        _userTokenDal = userTokenDal;
    }

    public async Task<int?> GetUserIdByToken()
    {
        string? tokenCookie = _webCoookie.Get(AuthConstants.RememberMeCookieName);
        if (tokenCookie == null) 
            return null;
        Guid? tokenGuid = Helpers.StringToGuidDef(tokenCookie ?? "");
        if (tokenGuid == null)
            return null;

        int? userId = await _userTokenDal.Get((Guid)tokenGuid);
        return userId;
    }

    public async Task<bool> IsLoggedIn()
    {
        bool isLoggedIn =  await _dbSession.IsLoggedIn();
        if (!isLoggedIn)
        {
            int? userId = await GetUserIdByToken();
            if (userId != null)
            {
                await _dbSession.SetUserId((int)userId);
                isLoggedIn = true;
            }
        }

        return isLoggedIn;
    }
}