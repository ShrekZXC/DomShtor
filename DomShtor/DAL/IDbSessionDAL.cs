using DomShtor.Models;

namespace DomShtor.DAL;

public interface IDbSessionDAL
{
    Task<SessionModel?> Get(Guid sessionId);
    Task Lock(Guid sessionId);

    Task<int> Update(SessionModel model);

    Task<int> Create(SessionModel model);
}