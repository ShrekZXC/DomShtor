using DomShtor.DAL.Models;

namespace DomShtor.DAL;

public interface IProfileDAL
{
    Task<ProfileModel> Get(int userId);
    Task<int> Add(ProfileModel profileModel);
    Task Update(ProfileModel profileModel);
}