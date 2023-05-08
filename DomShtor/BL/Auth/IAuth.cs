using System.ComponentModel.DataAnnotations;
using DomShtor.DAL.Models;

namespace DomShtor.BL.Auth;

public interface IAuth
{
    Task<int> CreateUser(UserModel user);
    Task<int> Authenticate(string email, string password, bool rememberMe);
    Task ValidateEmail(string email);
    Task ValidatePassword(string password, string reenterPassword);
    Task Register(UserModel userModel);
}