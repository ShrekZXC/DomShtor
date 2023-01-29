using DomShtor.DAL.Models;
using DomShtor.DAL;

namespace DomShtor.BL;

public class AuthBL: IAuthBL
{
    private readonly IAuthDAL _authDal;
    
    public AuthBL(IAuthDAL authDal)
    {
        _authDal = authDal;
    }

    public async Task<int> CreateUser(UserModel userModel)
    {
        return await _authDal.CreateUser(userModel);
    }
}