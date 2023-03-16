using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WildlifeAPI.Models;
using WildlifeAPI.Models;
using WildlifeAPI_Prod.Models;
using Microsoft.EntityFrameworkCore;

namespace WildlifeAPI.Data
{
    public class WildlifeAPIContext : DbContext
    {
        static string dbConnectionString = "";
        public WildlifeAPIContext (DbContextOptions<WildlifeAPIContext> options)
            : base(options)
        {
            const string kvURI = "https://amiel-wildlife-kvault.vault.azure.net/";
            const string secretName = "MySQL-Wildlife";
            var client = new SecretClient(new Uri(kvURI), new DefaultAzureCredential());
            var secretData = client.GetSecret(secretName);
            dbConnectionString = secretData.Value.Value;
        }

        public DbSet<WildlifeAPI.Models.Animals> Animals { get; set; } = default!;

        public DbSet<WildlifeAPI.Models.Blogs> Blogs { get; set; } = default!;

        public DbSet<WildlifeAPI.Models.Volunteers> Volunteers { get; set; } = default!;

        public DbSet<WildlifeAPI.Models.Programs> Programs { get; set; } = default!;

        public DbSet<WildlifeAPI_Prod.Models.Contacts> Contacts { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(dbConnectionString ?? throw new InvalidOperationException("Connection string 'WildlifeAPIContext' not found."));
        }
    }
}
