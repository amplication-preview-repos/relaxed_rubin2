using Microsoft.AspNetCore.Mvc;
using Users.APIs;
using Users.APIs.Common;
using Users.APIs.Dtos;
using Users.APIs.Errors;

namespace Users.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class NamesControllerBase : ControllerBase
{
    protected readonly INamesService _service;

    public NamesControllerBase(INamesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Name
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Name>> CreateName(NameCreateInput input)
    {
        var name = await _service.CreateName(input);

        return CreatedAtAction(nameof(Name), new { id = name.Id }, name);
    }

    /// <summary>
    /// Delete one Name
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteName([FromRoute()] NameWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteName(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Names
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Name>>> Names([FromQuery()] NameFindManyArgs filter)
    {
        return Ok(await _service.Names(filter));
    }

    /// <summary>
    /// Meta data about Name records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> NamesMeta([FromQuery()] NameFindManyArgs filter)
    {
        return Ok(await _service.NamesMeta(filter));
    }

    /// <summary>
    /// Get one Name
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Name>> Name([FromRoute()] NameWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Name(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Name
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateName(
        [FromRoute()] NameWhereUniqueInput uniqueId,
        [FromQuery()] NameUpdateInput nameUpdateDto
    )
    {
        try
        {
            await _service.UpdateName(uniqueId, nameUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
