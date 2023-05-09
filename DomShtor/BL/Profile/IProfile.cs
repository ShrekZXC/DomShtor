using DomShtor.DAL.Models;

namespace DomShtor.BL.Profile;

public interface IProfile
{
    Task<ProfileModel> GetByUserId(int userId);

    Task AddOrUpdate(ProfileModel profileModel);
}