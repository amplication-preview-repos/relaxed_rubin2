using Users.Infrastructure;

namespace Users.APIs;

public class NamesService : NamesServiceBase
{
    public NamesService(UsersDbContext context)
        : base(context) { }
}
