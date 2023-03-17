using Microsoft.EntityFrameworkCore;
using PageScrapper.Domain;
using System.Diagnostics.Metrics;

namespace PageScrapper.Infrastructure
{
    public class CountryRepository : ICountryRepository
    {
        private readonly CountryDbContext _dbContext;

        public CountryRepository(CountryDbContext context)
        {
            _dbContext = context;
        }

        public void DeleteAllCountriesFromDb()
        {
           foreach(Country temp in _dbContext.Countries)
            {
                _dbContext.Remove(temp);
            }
            _dbContext.SaveChanges();
        }

        public bool IsEmpty() => !(_dbContext.Countries.Any());

        public bool DeleteCountry(int id)
        {
            bool deleted = false;
            Country ?toDelete = _dbContext.Countries.Find(id);
            if (toDelete != null)
            {
                _dbContext.Countries.Remove(toDelete);
                deleted = true;
            }
            if (deleted)
            {
                _dbContext.SaveChanges();
            }
               
            return deleted;
        }

        public IEnumerable<Country> GetAllCountry() => _dbContext.Countries.ToList();


        public Country GetCountryById(int id) => _dbContext.Countries.Find(id);
      

        public bool InsertCountry(Country country)
        {
           _dbContext.Countries.Add(country);
           return _dbContext.SaveChanges() > 0 ? true : false;
        }

        public void SaveCoutries(IEnumerable<Country> countries)
        {
            foreach (Country country in countries) { 
                _dbContext.Countries.Add(country);
            }
            _dbContext.SaveChanges();
        }

        public bool ModifyCountry(Country country)
        {
            Country ?toModify = _dbContext.Countries.SingleOrDefault(c => c.Id == country.Id);
            bool modified = false;
            if (toModify != null)
            {
                toModify.Area = country.Area;
                toModify.Capital = country.Capital;
                toModify.Population = country.Population;
                toModify.Name = country.Name;
                _dbContext.SaveChanges();
                modified = true;

            }
            return modified;
            
        }
    }
}