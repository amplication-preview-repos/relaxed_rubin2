using Microsoft.AspNetCore.Mvc;
using Users.APIs.Common;
using Users.Infrastructure.Models;

namespace Users.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class NameFindManyArgs : FindManyInput<Name, NameWhereInput> { }
