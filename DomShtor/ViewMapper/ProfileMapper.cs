﻿using DomShtor.DAL.Models;
using DomShtor.ViewModels;

namespace DomShtor.ViewMapper;

public class ProfileMapper
{
    public static ProfileModel MapProfileViewModelToProfileModel(ProfileViewModel model)
    {
        return new ProfileModel()
        {
            ProfileId = model.ProfileId!,
            Email = model.Email!,
            FirstName = model.FirstName!,
            SecondName = model.SecondName!,
            LastName = model.LastName!
        };
    }
    
    public static ProfileViewModel MapProfileModelToProfileViewModel(ProfileModel model)
    {
        return new ProfileViewModel()
        {
            ProfileId = model.ProfileId!,
            Email = model.Email!,
            FirstName = model.FirstName!,
            SecondName = model.SecondName!,
            LastName = model.LastName!,
            ProfileImage = model.ProfileImage
        };
    }
    
    public static ProfileModel MapUserModelToProfileModel(UserModel model)
    {
        return new ProfileModel()
        {
            UserId = (int)model.UserId,
            Email = model.Email!,
            FirstName = model.FirstName!,
            SecondName = model.SecondName!,
            LastName = model.LastName!
        };
    }
}