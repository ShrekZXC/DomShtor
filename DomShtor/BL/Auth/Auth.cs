using System.ComponentModel.DataAnnotations;
using DomShtor.BL.General;
using DomShtor.DAL.Models;
using DomShtor.DAL;
using DomShtor.ViewMapper;

namespace DomShtor.BL.Auth;

public class Auth: IAuth
{
    private readonly IAuthDAL _authDal;
    private readonly IEncrypt _encrypt;
    private readonly IDbSession _dbSession;
    private readonly IWebCoookie _webCoookie;
    private readonly IUserTokenDAL _userTokenDal;
    private readonly IProfileDAL _profileDal;
    
    public Auth(IAuthDAL authDal, 
        IEncrypt encrypt,
        IDbSession dbSession,
        IWebCoookie webCoookie,
        IUserTokenDAL userTokenDal,
        IProfileDAL profileDal
    )
    {
        _authDal = authDal;
        _encrypt = encrypt;
        _dbSession = dbSession;
        _webCoookie = webCoookie;
        _userTokenDal = userTokenDal;
        _profileDal = profileDal;
    }

    public async Task<int> Authenticate(string email, string password, bool rememberMe)
    {
        var user = await _authDal.GetUser(email);
        
        if (user.UserId != null && user.Password == _encrypt.HashPassword(password, user.Salt))
        {
            await Login(user.UserId ?? 0);

            if (rememberMe)
            {
                Guid tokenId = await _userTokenDal.Create(user.UserId ?? 0);
                _webCoookie.AddSecure(AuthConstants.RememberMeCookieName, tokenId.ToString(), AuthConstants.RememberMeDays);
            }
            return user.UserId ?? 0;
        }

        throw new AuthorizationException("Not Found");
    }
    
    public async Task<int> CreateUser(UserModel userModel)
    {
        userModel.Salt = Guid.NewGuid().ToString();
        userModel.Password = _encrypt.HashPassword(userModel.Password, userModel.Salt);
        int id = await _authDal.CreateUser(userModel);
        userModel.UserId = id;
        await _profileDal.Add(ProfileMapper.MapUserModelToProfileModel(userModel));
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