using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WildlifeAPI.Models;
using WildlifeAPI.Models;

namespace WildlifeAPI.Data
{
    public class WildlifeAPIContext : DbContext
    {
        public WildlifeAPIContext (DbContextOptions<WildlifeAPIContext> options)
            : base(options)
        {
        }

        public DbSet<WildlifeAPI.Models.Animals> Animals { get; set; } = default!;

        public DbSet<WildlifeAPI.Models.Blogs> Blogs { get; set; } = default!;

        public DbSet<WildlifeAPI.Models.Volunteers> Volunteers { get; set; } = default!;

        public DbSet<WildlifeAPI.Models.Programs> Programs { get; set; } = default!;
    }
}
