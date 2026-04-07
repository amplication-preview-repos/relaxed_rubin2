using Microsoft.AspNetCore.Mvc;

namespace Users.APIs;

[ApiController()]
public class NamesController : NamesControllerBase
{
    public NamesController(INamesService service)
        : base(service) { }
}
