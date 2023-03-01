using WildlifeAPI.Models;

namespace WildlifeAPI_Prod.Data.Services
{
    public interface IProgramsService
    {
        Task<IEnumerable<Programs>> GetAll();
        Task<Programs> GetById(int id);
    }
}
