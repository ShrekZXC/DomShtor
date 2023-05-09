namespace DomShtor.DAL;

public class UserTokenDalDal : IUserTokenDAL
{
    public async Task<Guid> Create(int userId)
    {
        var tokenId = Guid.NewGuid();
        var sql = @"insert into UserToken (UserTokenId, UserId, Created)
                        values (@tokenId, @userId, NOW())";
        await DbHelper.ExecuteScalarAsync(sql, new { tokenId = tokenId, userId = userId});
        return tokenId;
    }

    public async Task<int?> Get(Guid tokenId)
    {
        var sql = @"select UserId from UserToken where UserTokenId = @tokenId";
        return await DbHelper.ExecuteScalarAsync(sql, new { tokenId = tokenId });
    }
}