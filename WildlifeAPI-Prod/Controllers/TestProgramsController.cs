using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WildlifeAPI.Models;
using WildlifeAPI_Prod.Data.Services;

namespace WildlifeAPI_Prod.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestProgramsController : ControllerBase
    {
        private readonly IProgramsService _service;
        public TestProgramsController(IProgramsService service)
        {
            _service = service;
        }

        // GET: api/Programs
        [HttpGet]
        public async Task<IEnumerable<Programs>> GetAllPrograms()
        {
            if (_service.GetAll() == null)
            {
                return (IEnumerable<Programs>)NotFound();
            }
            return await _service.GetAll();
        }

        // GET: api/Programs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Programs>> GetProgramsById(int id)
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
    }
}
