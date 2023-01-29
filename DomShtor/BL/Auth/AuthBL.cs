using DomShtor.DAL.Models;
using DomShtor.DAL;

namespace DomShtor.BL.Auth;

public class AuthBL: IAuthBL
{
    private readonly IAuthDAL _authDal;
    private readonly IEncrypt _encrypt;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AuthBL(IAuthDAL authDal, 
        IEncrypt encrypt, 
        IHttpContextAccessor httpContextAccessor
        )
    {
        _authDal = authDal;
        _encrypt = encrypt;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<int> CreateUser(UserModel userModel)
    {
        userModel.Salt = Guid.NewGuid().ToString();
        userModel.Password = _encrypt.HashPassword(userModel.Password, userModel.Salt);
        int id = await _authDal.CreateUser(userModel);
        Login(id);
        return id;
    }

    public void Login(int id)
    {
        _httpContextAccessor.HttpContext?.Session.SetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME, id);
    }
}