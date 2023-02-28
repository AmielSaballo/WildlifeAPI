﻿using System;
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
    public class AnimalsController : ControllerBase
    {
        private readonly WildlifeAPI_ProdContext _context;

        public AnimalsController(WildlifeAPI_ProdContext context)
        {
            _context = context;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animals>>> GetAnimals()
        {
          if (_context.Animals == null)
          {
              return NotFound();
          }
            return await _context.Animals.ToListAsync();
        }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Animals>> GetAnimals(int id)
        {
          if (_context.Animals == null)
          {
              return NotFound();
          }
            var animals = await _context.Animals.FindAsync(id);

            if (animals == null)
            {
                return NotFound();
            }

            return animals;
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimals(int id, Animals animals)
        {
            if (id != animals.id)
            {
                return BadRequest();
            }

            _context.Entry(animals).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalsExists(id))
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

        // POST: api/Animals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Animals>> PostAnimals(Animals animals)
        {
          if (_context.Animals == null)
          {
              return Problem("Entity set 'WildlifeAPI_ProdContext.Animals'  is null.");
          }
            _context.Animals.Add(animals);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimals", new { id = animals.id }, animals);
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimals(int id)
        {
            if (_context.Animals == null)
            {
                return NotFound();
            }
            var animals = await _context.Animals.FindAsync(id);
            if (animals == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animals);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimalsExists(int id)
        {
            return (_context.Animals?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
