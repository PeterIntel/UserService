using Microsoft.EntityFrameworkCore;

namespace User.Service.Entities
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
