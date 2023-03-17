using PageScrapper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageScrapper.Infrastructure
{
    public interface ICountryRepository
    {
        public bool InsertCountry(Country country);

        public Country GetCountryById(int id);

        public IEnumerable<Country> GetAllCountry();

        public bool DeleteCountry(int id);

        public void SaveCoutries(IEnumerable<Country> countries);

        public void DeleteAllCountriesFromDb();

        public bool IsEmpty();

        public bool ModifyCountry(Country country);
    }
}
