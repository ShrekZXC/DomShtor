namespace DomShtor.BL.Auth;

public class CurrentUser: ICurrentUser
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDbSession _dbSession;
    
    public CurrentUser(IHttpContextAccessor httpContextAccessor,
        IDbSession dbSession)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbSession = dbSession;
    }
    
    public async Task<bool> IsLoggedIn()
    {
        return await _dbSession.IsLoggedIn();
    }
}