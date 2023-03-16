using Microsoft.AspNetCore.Mvc;
using WildlifeAPI.Models;

namespace WildlifeAPI_Prod.Data.Services
{
    public interface IBlogsService
    {
        Task<IEnumerable<Blogs>> GetAll();
        Task<Blogs> GetById(int id);
    }
}
