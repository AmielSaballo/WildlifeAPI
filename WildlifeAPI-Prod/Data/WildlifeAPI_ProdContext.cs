using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WildlifeAPI.Models;

namespace WildlifeAPI_Prod.Data
{
    public class WildlifeAPI_ProdContext : DbContext
    {
        public WildlifeAPI_ProdContext (DbContextOptions<WildlifeAPI_ProdContext> options)
            : base(options)
        {
        }

        public DbSet<WildlifeAPI.Models.Animals> Animals { get; set; } = default!;

        public DbSet<WildlifeAPI.Models.Services> Services { get; set; } = default!;

        public DbSet<WildlifeAPI.Models.Blogs> Blogs { get; set; } = default!;

        public DbSet<WildlifeAPI.Models.Volunteers> Volunteers { get; set; } = default!;
    }
}
