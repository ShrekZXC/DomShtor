using DomShtor.DAL.Models;

namespace DomShtor.BL.Auth;

public interface ICurrentUser
{
    Task<bool> IsLoggedIn();
    
    Task<int?>GetCurrentUserId();

    Task<ProfileModel> GetProfile();
}