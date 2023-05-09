﻿namespace DomShtor.DAL;

public interface IUserTokenDAL
{
    Task<Guid> Create(int userId);

    Task<int?> Get(Guid tokenId);
}