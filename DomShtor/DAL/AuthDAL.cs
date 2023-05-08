using Dapper;
using DomShtor.DAL.Models;
using MySql.Data.MySqlClient;

namespace DomShtor.DAL
{

    public class AuthDAL: IAuthDAL
    {
        public async Task<UserModel> GetUser(string email)
        {
            var result = await DbHelper.QueryAsync<UserModel>($@"
                        select UserId, Email, Password, Salt, Status 
                        from appuser 
                        where Email = @email",
                        new {email = email});
                return result.FirstOrDefault() ?? new UserModel();
        }

        public async Task<UserModel> GetUser(int id)
        {
            using (var connection = new MySqlConnection(DbHelper.ConnString))
            {
                var result = await DbHelper.QueryAsync<UserModel>($@"
                        select UserId, Email, Password, Salt, Status 
                        from appuser 
                        where UserId = @id",
                        new {id = id});
                    return result.FirstOrDefault() ?? new UserModel();
            }
        }

        public async Task<int> CreateUser(UserModel model)
        {
            string sql = @"insert into appuser(Email, Password, Salt, FirstName, SecondName, LastName, Status)
                             values(@Email, @Password, @Salt, @FirstName, @SecondName, @LastName, @Status);
                select LAST_INSERT_ID();";
            var result = await DbHelper.QueryAsync<int>(sql, model);
            return result.First();
        }
    }
}