using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WildlifeAPI.Data;
using WildlifeAPI.Models;
using WildlifeAPI_Prod.Data.Services;

namespace WildlifeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private readonly IProgramsService _service;

        public ProgramsController(IProgramsService service)
        {
            _service = service;
        }

        // GET: api/Programs
        [HttpGet]
        public async Task<IEnumerable<Programs>> GetPrograms()
        {
            if (_service.GetAll() == null)
            {
                return (IEnumerable<Programs>)NotFound();
            }
            return await _service.GetAll();
        }

        // GET: api/Programs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Programs>> GetPrograms(int id)
        {
            var allPrograms = await _service.GetAll();

          if (allPrograms == null)
          {
              return NotFound();
          }
            var programs = await _service.GetById(id);

            if (programs == null)
            {
                return NotFound();
            }

            return programs;
        }

        //// PUT: api/Programs/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPrograms(int id, Programs programs)
        //{
        //    if (id != programs.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(programs).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProgramsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Programs
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Programs>> PostPrograms(Programs programs)
        //{
        //  if (_context.Programs == null)
        //  {
        //      return Problem("Entity set 'WildlifeAPIContext.Program'  is null.");
        //  }
        //    _context.Programs.Add(programs);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetPrograms", new { id = programs.id }, programs);
        //}

        //// DELETE: api/Programs/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePrograms(int id)
        //{
        //    if (_context.Programs == null)
        //    {
        //        return NotFound();
        //    }
        //    var programs = await _context.Programs.FindAsync(id);
        //    if (programs == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Programs.Remove(programs);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ProgramsExists(int id)
        //{
        //    return (_context.Programs?.Any(e => e.id == id)).GetValueOrDefault();
        //}
    }
}
