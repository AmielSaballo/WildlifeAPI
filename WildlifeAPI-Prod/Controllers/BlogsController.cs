using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WildlifeAPI.Models;
using WildlifeAPI.Data;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using WildlifeAPI_Prod.Data.Services;

namespace WildlifeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogsService _service;
        ILogger _logger;

        public BlogsController(IBlogsService service)
        {
            _service = service;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<IEnumerable<Blogs>> GetBlogs()
        {
          if (_service.GetAll() == null)
          {
              return (IEnumerable<Blogs>)NotFound();
          }
            return await _service.GetAll();
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blogs>> GetBlogs(int id)
        {
            var allBlogs = await _service.GetAll();

          if (allBlogs == null)
          {
              return NotFound();
          }
            var blogs = await _service.GetById(id);

            if (blogs == null)
            {
                return NotFound();
            }

            return blogs;
        }

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBlogs(int id, Blogs blogs)
        //{
        //    if (id != blogs.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(blogs).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BlogsExists(id))
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

        //// POST: api/Blogs
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Blogs>> PostBlogs(Blogs blogs)
        //{
        //  if (_context.Blogs == null)
        //  {
        //      return Problem("Entity set 'WildlifeAPIContext.Blogs'  is null.");
        //  }
        //    _context.Blogs.Add(blogs);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetBlogs", new { id = blogs.id }, blogs);
        //}

        //// DELETE: api/Blogs/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBlogs(int id)
        //{
        //    if (_context.Blogs == null)
        //    {
        //        return NotFound();
        //    }
        //    var blogs = await _context.Blogs.FindAsync(id);
        //    if (blogs == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Blogs.Remove(blogs);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool BlogsExists(int id)
        //{
        //    return (_context.Blogs?.Any(e => e.id == id)).GetValueOrDefault();
        //}
    }
}
