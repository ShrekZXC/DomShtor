using DomShtor.DAL.Models;
using DomShtor.DAL.Models;

namespace DomShtor.DAL;

public interface IAuthDAL
{
    Task<UserModel> GetUser(string email);
    Task<UserModel> GetUser(int id);
    Task<int> CreateUser(UserModel model);
}