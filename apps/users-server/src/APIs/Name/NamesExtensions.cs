using Users.APIs.Dtos;
using Users.Infrastructure.Models;

namespace Users.APIs.Extensions;

public static class NamesExtensions
{
    public static Name ToDto(this NameDbModel model)
    {
        return new Name
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static NameDbModel ToModel(this NameUpdateInput updateDto, NameWhereUniqueInput uniqueId)
    {
        var name = new NameDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            name.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            name.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return name;
    }
}
