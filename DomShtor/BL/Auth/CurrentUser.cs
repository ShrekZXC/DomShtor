using DomShtor.BL.General;
using DomShtor.BL.Profile;
using DomShtor.DAL;
using DomShtor.DAL.Models;

namespace DomShtor.BL.Auth;

public class CurrentUser: ICurrentUser
{
    
    private readonly IDbSession _dbSession;
    private readonly IWebCoookie _webCoookie;
    private readonly IUserTokenDAL _userTokenDal;
    private readonly IProfileDAL _profileDal;

    public CurrentUser(
        IDbSession dbSession,
        IWebCoookie webCoookie,
        IUserTokenDAL userTokenDal,
        IProfileDAL profileDal)
    {
        _dbSession = dbSession;
        _webCoookie = webCoookie;
        _userTokenDal = userTokenDal;
        _profileDal = profileDal;
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

    public async Task<int?> GetCurrentUserId()
    {
        var isLoggedIn = await IsLoggedIn();
        return (int)await _dbSession.GetUserId();
    }

    public async Task<ProfileModel> GetProfile()
    {
        var userId = await GetCurrentUserId();

        if (userId == null)
            throw new Exception("Пользователь не найден");
        
        return  await _profileDal.Get((int)userId);
    }
}