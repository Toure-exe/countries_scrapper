using Microsoft.EntityFrameworkCore;
using PageScrapper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageScrapper.Infrastructure
{
    public class CountryDbContext : DbContext 
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Localization> Localization { get; set; }
        public CountryDbContext(DbContextOptions<CountryDbContext> options) : base(options)
        {
               
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        {
            options.UseSqlServer("Data Source = XXXXX\\MSSQLSERVER01; Initial Catalog= db_paesi; Integrated Security=true; TrustServerCertificate=True");
        }
    }
}
