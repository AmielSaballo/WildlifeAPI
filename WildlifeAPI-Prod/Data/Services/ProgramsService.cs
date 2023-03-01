using Microsoft.EntityFrameworkCore;
using WildlifeAPI.Data;
using WildlifeAPI.Models;

namespace WildlifeAPI_Prod.Data.Services
{
    public class ProgramsService : IProgramsService
    {
        private readonly WildlifeAPIContext _context;
        public ProgramsService(WildlifeAPIContext context)
        {
            _context= context;
        }
        public async Task<IEnumerable<Programs>> GetAll()
        {
            var result = await _context.Programs.ToListAsync();
            return result;
        }

        public async Task<Programs> GetById(int id)
        {
            var result = await _context.Programs.FirstOrDefaultAsync(n => n.id == id);
            return result;
        }
    }
}
