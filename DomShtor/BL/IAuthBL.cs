using DomShtor.DAL.Models;

namespace DomShtor.BL;

public interface IAuthBL
{
    Task<int> CreateUser(UserModel user);
}