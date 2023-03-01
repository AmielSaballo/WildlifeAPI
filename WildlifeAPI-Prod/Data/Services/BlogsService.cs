using Microsoft.EntityFrameworkCore;
using WildlifeAPI.Data;
using WildlifeAPI.Models;

namespace WildlifeAPI_Prod.Data.Services
{
    public class BlogsService : IBlogsService
    {
        private readonly WildlifeAPIContext _context;
        public BlogsService(WildlifeAPIContext context)
        {
            _context= context;
        }

        public async Task<IEnumerable<Blogs>> GetAll()
        {
            var result = await _context.Blogs.ToListAsync();
            return result;
        }

        public async Task<Blogs> GetById(int id)
        {
            var result = await _context.Blogs.FirstOrDefaultAsync(n => n.id== id);
            return result;
        }
    }
}
