using DomShtor.DAL.Models;
using DomShtor.ViewModels;

namespace DomShtor.ViewMapper;

public class AuthMapper
{
    public static UserModel MapRegisterViewModelToUserModel(RegisterViewModel model)
    {
        return new UserModel()
        {
            Email = model.Email!,
            Password = model.Password!,
            ReenterPassword = model.ReenterPassword,
            FirstName = model.FirstName!,
            SecondName = model.SecondName!,
            LastName = model.LastName!
        };
    }
}