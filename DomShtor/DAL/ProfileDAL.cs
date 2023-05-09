using DomShtor.DAL.Models;
using MySql.Data.MySqlClient;

namespace DomShtor.DAL;

public class ProfileDAL : IProfileDAL
{
    public async Task<ProfileModel> Get(int userId)
    {
        using (var connection = new MySqlConnection(DbHelper.ConnString))
        {
            var result = await DbHelper.QueryAsync<ProfileModel>($@"
                        select ProfileId, UserId,Email, FirstName, SecondName, LastName, ProfileImage 
                        from Profile
                        where UserId = @id",
                new { id = userId });
            return result.FirstOrDefault() ?? new ProfileModel();
        }
    }

    public async Task<int> Add(ProfileModel profileModel)
    {
        string sql = @"insert into Profile(UserId,Email, FirstName, SecondName, LastName, ProfileImage )
                             values(@UserId, @Email, @FirstName, @SecondName, @LastName, @ProfileImage);
                select LAST_INSERT_ID();";
        var result = await DbHelper.QueryAsync<int>(sql, profileModel);
        return result.First();
    }

    public async Task Update(ProfileModel profileModel)
    {
        string sql = @"Update Profile
                    Set Email = @Email, 
                        FirstName = @FirstName, 
                        SecondName = @SecondName, 
                        LastName = @LastName, 
                        ProfileImage = @ProfileImage
                    where ProfileId = @ProfileId";
        var result = await DbHelper.QueryAsync<int>(sql, profileModel);
    }
}