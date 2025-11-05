using Microsoft.AspNetCore.Mvc;
using DBFirstApproach.Context;   // Namespace where ApplicationDbContext is defined
using DBFirstApproach.Models;    // Namespace where your entity classes (Color, Comment, Marka) are located
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBFirstApproach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetColors()
        {
            var colors = await _context.Colors.ToListAsync();
            return Ok(colors);
        }

        // ✅ GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Color>> GetColor(long id)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color == null)
                return NotFound();

            return Ok(color);
        }

        // ✅ POST api/values
        [HttpPost]
        public async Task<ActionResult<Color>> PostColor([FromBody] Color color)
        {
            _context.Colors.Add(color);
            await _context.SaveChangesAsync();

            // Returns 201 Created with the new record
            return CreatedAtAction(nameof(GetColor), new { id = color.Id }, color);
        }

        // ✅ PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColor(long id, [FromBody] Color color)
        {
            if (id != color.Id)
                return BadRequest("ID mismatch");

            _context.Entry(color).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Colors.AnyAsync(c => c.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // ✅ DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(long id)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color == null)
                return NotFound();

            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
