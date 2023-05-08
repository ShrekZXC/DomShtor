using System.ComponentModel.DataAnnotations;
using DomShtor.DAL.Models;
using DomShtor.DAL;

namespace DomShtor.BL.Auth;

public class AuthBL: IAuthBL
{
    private readonly IAuthDAL _authDal;
    private readonly IEncrypt _encrypt;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDbSession _dbSession;
    
    public AuthBL(IAuthDAL authDal, 
        IEncrypt encrypt, 
        IHttpContextAccessor httpContextAccessor,
        IDbSession dbSession
        )
    {
        _authDal = authDal;
        _encrypt = encrypt;
        _httpContextAccessor = httpContextAccessor;
        _dbSession = dbSession;
    }

    public async Task<int> CreateUser(UserModel userModel)
    {
        userModel.Salt = Guid.NewGuid().ToString();
        userModel.Password = _encrypt.HashPassword(userModel.Password, userModel.Salt);
        int id = await _authDal.CreateUser(userModel);
        Login(id);
        return id;
    }

    public async Task<int> Authenticate(string email, string password, bool rememberMe)
    {
        var user = await _authDal.GetUser(email);
        
        if (user.UserId != null && user.Password == _encrypt.HashPassword(password, user.Salt))
        {
            await Login(user.UserId ?? 0);
            return user.UserId ?? 0;
        }

        throw new AuthorizationException("Not Found");
    }

    public async Task<ValidationResult> ValidateEmail(string email)
    {
        var user = await _authDal.GetUser(email);
        if (user.UserId != null)
            return new ValidationResult("Email занят");
        return null;
    }

    public async Task<ValidationResult> ValidatePassword(string password, string reenterPassword)
    {
        if (password != reenterPassword)
            return new ValidationResult("Пароли должны совпадать");
        return null;
    }

    public async Task Login(int id)
    {
        await _dbSession.SetUserId(id);
    }
}