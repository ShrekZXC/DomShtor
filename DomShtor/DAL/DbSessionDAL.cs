using Dapper;
using DomShtor.Models;
using MySql.Data.MySqlClient;

namespace DomShtor.DAL;

public class DbSessionDAL : IDbSessionDAL
{
    public async Task<int> CreateSession(SessionModel model)
    {
        using (var connection = new MySqlConnection(DbHelper.ConnString))
        {
            await connection.OpenAsync();
            string sql = @"insert into DbSession (DbSessionId, SessionData, Created, LastAccessed, UserId)
                        values (@DbSessionId, @SessionContent, @Created, @LastAccessed, @UserId)";

            return await connection.ExecuteAsync(sql, model);
        }
    }

    public async Task<SessionModel?> GetSession(Guid sessionId)
    {
        using (var connection = new MySqlConnection(DbHelper.ConnString))
        {
            await connection.OpenAsync();
            string sql = @"select DbSessionId, SessionData, Created, LastAccessed, UserId 
                            from DbSession 
                            where DbSessionID = @sessionId";

            var sessions = await connection.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
            return sessions.FirstOrDefault();
        }
    }

    public async Task<int> UpdateSession(SessionModel model)
    {
        using (var connection = new MySqlConnection(DbHelper.ConnString))
        {
            await connection.OpenAsync();
            string sql = @"update DbSession
                      set SessionData = @SessionData, LastAccessed = @LastAccessed, UserId = @UserId
                      where DbSessionID = @DbSessionID
                ";

            return await connection.ExecuteAsync(sql, model);
        }
    }
}