using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WildlifeAPI.Models;
using WildlifeAPI_Prod.Data.Services;

namespace WildlifeAPI_Prod.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestBlogsController : ControllerBase
    {
        private readonly IBlogsService _service;
        public TestBlogsController(IBlogsService service)
        {
            _service = service;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<IEnumerable<Blogs>> GetAllBlogs()
        {
            if (_service.GetAll() == null)
            {
                return (IEnumerable<Blogs>)NotFound();
            }
            return await _service.GetAll();
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blogs>> GetBlogsById(int id)
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
    }
}
