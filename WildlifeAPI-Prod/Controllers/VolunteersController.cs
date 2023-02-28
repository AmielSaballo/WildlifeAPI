using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WildlifeAPI.Models;
using WildlifeAPI_Prod.Data;

namespace WildlifeAPI_Prod.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteersController : ControllerBase
    {
        private readonly WildlifeAPI_ProdContext _context;

        public VolunteersController(WildlifeAPI_ProdContext context)
        {
            _context = context;
        }

        // GET: api/Volunteers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Volunteers>>> GetVolunteers()
        {
          if (_context.Volunteers == null)
          {
              return NotFound();
          }
            return await _context.Volunteers.ToListAsync();
        }

        // GET: api/Volunteers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Volunteers>> GetVolunteers(int id)
        {
          if (_context.Volunteers == null)
          {
              return NotFound();
          }
            var volunteers = await _context.Volunteers.FindAsync(id);

            if (volunteers == null)
            {
                return NotFound();
            }

            return volunteers;
        }

        // PUT: api/Volunteers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVolunteers(int id, Volunteers volunteers)
        {
            if (id != volunteers.id)
            {
                return BadRequest();
            }

            _context.Entry(volunteers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Volunteers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Volunteers>> PostVolunteers(Volunteers volunteers)
        {
          if (_context.Volunteers == null)
          {
              return Problem("Entity set 'WildlifeAPI_ProdContext.Volunteers'  is null.");
          }
            _context.Volunteers.Add(volunteers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVolunteers", new { id = volunteers.id }, volunteers);
        }

        // DELETE: api/Volunteers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVolunteers(int id)
        {
            if (_context.Volunteers == null)
            {
                return NotFound();
            }
            var volunteers = await _context.Volunteers.FindAsync(id);
            if (volunteers == null)
            {
                return NotFound();
            }

            _context.Volunteers.Remove(volunteers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VolunteersExists(int id)
        {
            return (_context.Volunteers?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
