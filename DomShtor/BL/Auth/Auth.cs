using System.ComponentModel.DataAnnotations;
using DomShtor.BL.General;
using DomShtor.DAL.Models;
using DomShtor.DAL;

namespace DomShtor.BL.Auth;

public class Auth: IAuth
{
    private readonly IAuthDAL _authDal;
    private readonly IEncrypt _encrypt;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDbSession _dbSession;
    
    public Auth(IAuthDAL authDal, 
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
    
    public async Task<int> CreateUser(UserModel userModel)
    {
        userModel.Salt = Guid.NewGuid().ToString();
        userModel.Password = _encrypt.HashPassword(userModel.Password, userModel.Salt);
        int id = await _authDal.CreateUser(userModel);
        Login(id);
        return id;
    }

    public async Task ValidateEmail(string email)
    {
        var user = await _authDal.GetUser(email);
        if (user.UserId != null)
            throw new DuplicateEmailException("Email уже существует");
    }

    public async Task ValidatePassword(string password, string reenterPassword)
    {
        if (password != reenterPassword)
            throw new MismatchedPasswordException("Пароли должны совпадать");
    }

    public async Task Login(int id)
    {
        await _dbSession.SetUserId(id);
    }

    public async Task Register(UserModel userModel)
    {
        using (var scope = Helpers.CreateTransactionScope())
        {
            await _dbSession.Lock();
            await ValidateEmail(userModel.Email);
            await ValidatePassword(userModel.Password, userModel.ReenterPassword);
            await CreateUser(userModel);
            scope.Complete();
        }
    }
}