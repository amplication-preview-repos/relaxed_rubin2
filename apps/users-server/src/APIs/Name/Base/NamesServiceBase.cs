using Microsoft.EntityFrameworkCore;
using Users.APIs;
using Users.APIs.Common;
using Users.APIs.Dtos;
using Users.APIs.Errors;
using Users.APIs.Extensions;
using Users.Infrastructure;
using Users.Infrastructure.Models;

namespace Users.APIs;

public abstract class NamesServiceBase : INamesService
{
    protected readonly UsersDbContext _context;

    public NamesServiceBase(UsersDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Name
    /// </summary>
    public async Task<Name> CreateName(NameCreateInput createDto)
    {
        var name = new NameDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            name.Id = createDto.Id;
        }

        _context.Names.Add(name);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<NameDbModel>(name.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Name
    /// </summary>
    public async Task DeleteName(NameWhereUniqueInput uniqueId)
    {
        var name = await _context.Names.FindAsync(uniqueId.Id);
        if (name == null)
        {
            throw new NotFoundException();
        }

        _context.Names.Remove(name);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Names
    /// </summary>
    public async Task<List<Name>> Names(NameFindManyArgs findManyArgs)
    {
        var names = await _context
            .Names.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return names.ConvertAll(name => name.ToDto());
    }

    /// <summary>
    /// Meta data about Name records
    /// </summary>
    public async Task<MetadataDto> NamesMeta(NameFindManyArgs findManyArgs)
    {
        var count = await _context.Names.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Name
    /// </summary>
    public async Task<Name> Name(NameWhereUniqueInput uniqueId)
    {
        var names = await this.Names(
            new NameFindManyArgs { Where = new NameWhereInput { Id = uniqueId.Id } }
        );
        var name = names.FirstOrDefault();
        if (name == null)
        {
            throw new NotFoundException();
        }

        return name;
    }

    /// <summary>
    /// Update one Name
    /// </summary>
    public async Task UpdateName(NameWhereUniqueInput uniqueId, NameUpdateInput updateDto)
    {
        var name = updateDto.ToModel(uniqueId);

        _context.Entry(name).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Names.Any(e => e.Id == name.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
