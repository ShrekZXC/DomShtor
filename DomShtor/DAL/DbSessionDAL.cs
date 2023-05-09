using Dapper;
using DomShtor.Models;
using MySql.Data.MySqlClient;

namespace DomShtor.DAL;

public class DbSessionDAL : IDbSessionDAL
{
    public async Task Create(SessionModel model)
    {
        var sql = @"insert into DbSession (DbSessionId, SessionData, Created, LastAccessed, UserId)
                        values (@DbSessionId, @SessionContent, @Created, @LastAccessed, @UserId)";
        await DbHelper.ExecuteAsync(sql, model);
    }

    public async Task<SessionModel?> Get(Guid sessionId)
    {
        string sql =
            @"select DbSessionID, SessionData, Created, LastAccessed, UserId from DbSession where DbSessionID = @sessionId";
        var session = await DbHelper.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
        return session.FirstOrDefault();
    }

    public async Task Lock(Guid sessionId)
    {
        string sql = @"select DbSessionId
                            from DbSession 
                            where DbSessionID = @sessionId
                            for update";

        await DbHelper.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
    }

    public async Task Update(SessionModel model)
    {
        string sql = @"update DbSession
                      set SessionData = @SessionData, LastAccessed = @LastAccessed, UserId = @UserId
                      where DbSessionID = @DbSessionID
                ";

        await DbHelper.ExecuteAsync(sql, model);
    }
}