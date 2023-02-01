using DomShtor.DAL.Models;

namespace DomShtor.BL.Auth;

public interface IAuthBL
{
    Task<int> CreateUser(UserModel user);
    Task<int> Authenticate(string email, string password, bool rememberMe);
}