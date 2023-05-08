using Dapper;
using DomShtor.Models;
using MySql.Data.MySqlClient;

namespace DomShtor.DAL;

public class DbSessionDAL : IDbSessionDAL
{
    public async Task<int> Create(SessionModel model)
    {
        using (var connection = new MySqlConnection(DbHelper.ConnString))
        {
            await connection.OpenAsync();
            string sql = @"insert into DbSession (DbSessionId, SessionData, Created, LastAccessed, UserId)
                        values (@DbSessionId, @SessionContent, @Created, @LastAccessed, @UserId)";

            return await connection.ExecuteAsync(sql, model);
        }
    }

    public async Task<SessionModel?> Get(Guid sessionId)
    {
        using (var connection = new MySqlConnection(DbHelper.ConnString))
        {
            await connection.OpenAsync();
            string sql = @"select DbSessionID, SessionData, Created, LastAccessed, UserId from DbSession where DbSessionID = @sessionId";

            var sessions = await connection.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
            return sessions.FirstOrDefault();
        }
    }

    public async Task Lock(Guid sessionId)
    {
        using (var connection = new MySqlConnection(DbHelper.ConnString))
        {
            await connection.OpenAsync();
            string sql = @"select DbSessionId
                            from DbSession 
                            where DbSessionID = @sessionId
                            for update";

            await connection.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
        }
    }

    public async Task<int> Update(SessionModel model)
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