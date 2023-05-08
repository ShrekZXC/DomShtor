using DomShtor.BL.Auth;
using DomShtor.DAL;
using Microsoft.AspNetCore.Http;

namespace DomShtorTest.Helpers;

public class BaseTest
{
    protected IAuthDAL _authDal = new AuthDAL();
    protected IEncrypt _encrypt = new Encrypt();
    protected IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
    protected IAuthBL _authBl;
    protected IDbSessionDAL _dbSessionDal = new DbSessionDAL();
    protected IDbSession _dbSession;

    public BaseTest()
    {
        _dbSession = new DbSession(_dbSessionDal, _httpContextAccessor);
        _authBl = new AuthBL(_authDal, _encrypt, _httpContextAccessor, _dbSession);
    }
    
}