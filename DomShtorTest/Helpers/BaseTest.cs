using DomShtor.BL.Auth;
using DomShtor.BL.General;
using DomShtor.DAL;
using Microsoft.AspNetCore.Http;

namespace DomShtorTest.Helpers;

public class BaseTest
{
    protected IAuthDAL _authDal = new AuthDAL();
    protected IEncrypt _encrypt = new Encrypt();
    protected IAuth Auth;
    protected IDbSessionDAL _dbSessionDal = new DbSessionDAL();
    protected IDbSession _dbSession;
    protected IWebCoookie _webCoookie;
    protected IUserTokenDAL _userTokenDal = new UserTokenDalDal();

    public BaseTest()
    {
        _webCoookie = new TestCookie();
        _dbSession = new DbSession(_dbSessionDal, _webCoookie);
        Auth = new Auth(_authDal, _encrypt ,_dbSession,_webCoookie, _userTokenDal);
    }
    
}