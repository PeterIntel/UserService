using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Service.CustomExtensionMethods;
using User.Service.Entities;

namespace User.Service.Repositories
{
    public class RemoteUserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public RemoteUserRepository(UserDbContext context)
        {
            _context = context;
        }

        private Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<List<UserEntity>> GetAll()
        {
            var users = _context.Users.ToListAsync();

            return users;
        }

        public Task<UserEntity> GetSingleById(Guid id)
        {
            return _context.Users.FirstOrExceptionAsync<UserEntity>(id);
        }

        public Task Add(UserEntity user)
        {
            user.Id = Guid.NewGuid();
            _context.Users.Add(user);

            return SaveAsync();
        }

        public async Task Update(UserEntity user)
        {
            var userToUpdate = await GetSingleById(user.Id);
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            _context.Users.Update(userToUpdate);

            await SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            var user = await GetSingleById(id);
            _context.Users.Remove(user);

            await SaveAsync();
        }
    }
}
