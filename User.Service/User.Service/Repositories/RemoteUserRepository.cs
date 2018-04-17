using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Service.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace User.Service.Repositories
{
    public class RemoteUserRepository : IUserRepository
    {
        //todo: Complete the implementation of this rep
        private readonly UserDbContext _context;

        public RemoteUserRepository(UserDbContext context)
        {
            _context = context;
        }

        private async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users.ToListAsync();

            return new OkObjectResult(users);
        }

        public async Task<IActionResult> GetSingle(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            return new OkObjectResult(user);
        }

        public async Task<IActionResult> Add(UserEntity user)
        {
            user.Id = Guid.NewGuid();
            _context.Users.Add(user);
            await SaveAsync();

            return new OkResult();
        }

        public async Task<IActionResult> Update(UserEntity user)
        {
            _context.Users.Update(user);
            await SaveAsync();

            return new OkResult();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var user = GetSingle(id);
            //_context.Users.Remove(user.);
            await SaveAsync();

            return new OkResult();
        }
    }
}
