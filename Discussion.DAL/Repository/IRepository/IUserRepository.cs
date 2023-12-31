﻿using Discussion.Entities;

namespace Discussion.DAL.Repository.IRepository;

public interface IUserRepository : IRepository<UserEntity>
{
    new Task Remove(UserEntity userEntity);
}
