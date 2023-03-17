using PageScrapper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageScrapper
{
    public interface IScrapperService
    {
        public IEnumerable<Country> GetCountryListFromTarget(string targetUrl);
        
        public void SaveCountries();

        public bool SaveCountry(Country country);

        public bool DeleteCountry(int id);

        public bool DeleteAllCountriesFromDb();

        public Country GetCountryById(int id);

        public IEnumerable<Country> GetCountryList();

        public bool ModifyCountry(int id, string toModify, string value);

    }
}
