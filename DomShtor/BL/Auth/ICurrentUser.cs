namespace DomShtor.BL.Auth;

public interface ICurrentUser
{
    Task<bool> IsLoggedIn();
}