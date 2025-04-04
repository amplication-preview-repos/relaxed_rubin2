using Users.APIs.Common;
using Users.APIs.Dtos;

namespace Users.APIs;

public interface INamesService
{
    /// <summary>
    /// Create one Name
    /// </summary>
    public Task<Name> CreateName(NameCreateInput name);

    /// <summary>
    /// Delete one Name
    /// </summary>
    public Task DeleteName(NameWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Names
    /// </summary>
    public Task<List<Name>> Names(NameFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Name records
    /// </summary>
    public Task<MetadataDto> NamesMeta(NameFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Name
    /// </summary>
    public Task<Name> Name(NameWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Name
    /// </summary>
    public Task UpdateName(NameWhereUniqueInput uniqueId, NameUpdateInput updateDto);
}
