using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Service.Entities;

namespace User.Service.Repositories
{
    public interface IUserRepository
    {
        Task Add(UserEntity user);

        Task Delete(Guid id);

        Task<List<UserEntity>> GetAll();

        Task<UserEntity> GetSingleById(Guid id);

        Task Update(UserEntity user);
    }
}