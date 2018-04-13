using System;
using System.Linq;
using User.Service.Entities;

namespace User.Service.Repositories
{
    public interface IUserRepository
    {
        void Add(UserEntity user);

        void Delete(Guid id);

        IQueryable<UserEntity> GetAll();

        UserEntity GetSingle(Guid id);

        bool Save();

        void Update(UserEntity user);
    }
}