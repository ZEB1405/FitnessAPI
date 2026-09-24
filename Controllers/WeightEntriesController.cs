using Microsoft.AspNetCore.Mvc;
using FitnessApi.Models;
using FitnessApi.Data;
using Microsoft.EntityFrameworkCore;


namespace FitnessApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeightEntriesController : ControllerBase
{
    private readonly WeightTrackerContext _context;

    public WeightEntriesController(WeightTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all weight entries.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<WeightEntry>>> GetWeightEntries(string? sort)
    {
        var query = _context.WeightEntries.AsQueryable();

        if (string.Equals(sort, "asc", StringComparison.OrdinalIgnoreCase))
        {
            query = query.OrderBy(w => w.RecordedAt);
        }
        else if (string.Equals(sort, "desc", StringComparison.OrdinalIgnoreCase))
        {
            query = query.OrderByDescending(w => w.RecordedAt);
        }
        else if (!string.IsNullOrWhiteSpace(sort))
        {
            return BadRequest("Invalid sort parameter. Use 'asc' or 'desc'.");
        }

        var weightEntries = await query.ToListAsync();
        return Ok(weightEntries);
    }

    /// <summary>
    /// Retrieves a specific weight entry by its ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<WeightEntry>> GetWeightEntry(int id)
    {
        var weightEntry = await _context.WeightEntries.FindAsync(id);

        if (weightEntry is null)
        {
            return NotFound();
        }
        return Ok(weightEntry);
    }

    /// <summary>
    /// Creates a new weight entry.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<WeightEntry>> CreateWeightEntry(WeightEntry weightEntry)
    {
        if (weightEntry.RecordedAt > DateTime.UtcNow)
        {
            return BadRequest("RecordedAt cannot be in the future.");
        }

        _context.WeightEntries.Add(weightEntry);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetWeightEntry), new { id = weightEntry.Id}, weightEntry);
    }

    /// <summary>
    /// Deletes a weight entry by its ID.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteWeightEntry(int id)
    {
        var weightEntry = await _context.WeightEntries.FindAsync(id);

        if (weightEntry is null)
        {
            return NotFound();
        }

        _context.WeightEntries.Remove(weightEntry);
        await _context.SaveChangesAsync();

        return NoContent();

    }
}
    