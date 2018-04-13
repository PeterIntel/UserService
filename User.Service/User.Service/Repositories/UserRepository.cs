using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Service.Entities;

namespace User.Service.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public IQueryable<UserEntity> GetAll()
        {
            return _context.Users;
        }

        public UserEntity GetSingle(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public void Add(UserEntity user)
        {
            _context.Users.Add(user);
        }

        public void Update(UserEntity user)
        {
            _context.Users.Update(user);
        }

        public void Delete(Guid id)
        {
            UserEntity user = GetSingle(id);
            _context.Users.Remove(user);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
