using DomShtor.DAL;
using DomShtor.DAL.Models;

namespace DomShtor.BL.Profile;

public class Profile: IProfile
{
    private IProfileDAL _profileDal;

    public Profile(IProfileDAL profileDal)
    {
        _profileDal = profileDal;
    }
    public async Task<ProfileModel> GetByUserId(int userId)
    {
        return await _profileDal.Get(userId);
    }

    public async Task AddOrUpdate(ProfileModel profileModel)
    {
        if (profileModel.ProfileId == null)
            profileModel.ProfileId = await _profileDal.Add(profileModel);
        else
            await _profileDal.Update(profileModel);
    }
}