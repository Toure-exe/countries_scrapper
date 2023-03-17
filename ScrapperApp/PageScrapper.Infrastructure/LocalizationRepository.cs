using PageScrapper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageScrapper.Infrastructure
{
    public class LocalizationRepository : ILocalizationRepository
    {
        private readonly CountryDbContext _dbContext;

        public LocalizationRepository(CountryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void InsertAddress(string address)
        {
            Localization loc = new Localization();
            loc.ip_address = address;
            _dbContext.Localization.Add(loc);
            _dbContext.SaveChanges();
        }

        public void InsertLocalization(Localization loc)
        {
            var result = (from countries in _dbContext.Countries
                         from locDb in _dbContext.Localization
                         where countries.Name == locDb.Country
                         select new { id = countries.Id }).ToList();

            loc.Country_id = result.First().id;
            _dbContext.Localization.Add(loc);
            _dbContext.SaveChanges();
        }
    }
}
