using Microsoft.EntityFrameworkCore;
using Users.Infrastructure.Models;

namespace Users.Infrastructure;

public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options)
        : base(options) { }

    public DbSet<NameDbModel> Names { get; set; }
}
