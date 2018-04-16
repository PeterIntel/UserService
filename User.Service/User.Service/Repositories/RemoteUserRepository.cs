using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Service.Entities;
using Microsoft.EntityFrameworkCore;

namespace User.Service.Repositories
{
    public class RemoteUserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public RemoteUserRepository(UserDbContext context)
        {
            _context = context;
        }

        private async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public UserEntity GetSingle(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public async Task Add(UserEntity user)
        {
            user.Id = Guid.NewGuid();
            _context.Users.Add(user);
            await SaveAsync();
        }

        public async Task Update(UserEntity user)
        {
            _context.Users.Update(user);
            await SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            UserEntity user = GetSingle(id);
            _context.Users.Remove(user);
            await SaveAsync();
        }
    }
}
