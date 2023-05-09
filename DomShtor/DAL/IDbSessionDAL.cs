using DomShtor.Models;

namespace DomShtor.DAL;

public interface IDbSessionDAL
{
    Task<SessionModel?> Get(Guid sessionId);
    Task Lock(Guid sessionId);

    Task Update(SessionModel model);

    Task Create(SessionModel model);
}