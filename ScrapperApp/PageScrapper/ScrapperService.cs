using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PageScrapper.Domain;
using PageScrapper.Infrastructure;

namespace PageScrapper
{
    public class ScrapperService : IScrapperService
    {
        private readonly List<Country> countries = new List<Country>();


        private readonly IDownloaderService _downloaderService;
        private readonly ICountryRepository _countryRepository;
        public string HtmlCode { get; set; }


        public ScrapperService(IDownloaderService downloaderService, ICountryRepository countryRepository)
        {
            _downloaderService = downloaderService;
            _countryRepository= countryRepository;
            HtmlCode= string.Empty;

        }
        public IEnumerable<Country> GetCountryListFromTarget(string targetUrl)
        {
            HtmlCode = _downloaderService.DownloadHtmlAsync(targetUrl).Result;
            var doc = new HtmlDocument();
            doc.LoadHtml(HtmlCode);
            HtmlNodeCollection countriesResult = doc.DocumentNode.SelectNodes("//*[@id=\"countries\"]/div/div[position()>3]/div/h3");
            HtmlNodeCollection capitales = doc.DocumentNode.SelectNodes("//*[@id=\"countries\"]/div/div[position()>3]/div/div/span[1]");
            HtmlNodeCollection population = doc.DocumentNode.SelectNodes("//*[@id=\"countries\"]/div/div[position()>3]/div/div/span[2]");
            HtmlNodeCollection area = doc.DocumentNode.SelectNodes("//*[@id=\"countries\"]/div/div[position()>3]/div/div/span[3]");

            int i = 0;
            foreach(HtmlNode name in countriesResult)
            {
                this.countries.Add(new Country(name.InnerText.Trim(),
                    capitales.ElementAt(i).InnerText.Trim(), 
                    int.Parse(population.ElementAt(i).InnerText.Trim()),
                    double.Parse(area.ElementAt(i).InnerText.Trim())));
                i++;
            }



            return this.countries;
        }

        public void SaveCountries()
        {
            _countryRepository.SaveCoutries(this.countries);
        }

        public bool SaveCountry(Country country) 
            => (country != null) ? _countryRepository.InsertCountry(country) : false;
           
       

        public IEnumerable<Country> GetCountryList() => this.countries; 

        public bool DeleteCountry(int id)
            => (id is int and > (-1)) ? _countryRepository.DeleteCountry(id) : false;
        
        public Country? GetCountryById(int id)
            => (id is int and > (-1)) ? _countryRepository.GetCountryById(id) : null;


        public bool DeleteAllCountriesFromDb()
        {
            bool empty = _countryRepository.IsEmpty();
            if (!empty)
                _countryRepository.DeleteAllCountriesFromDb();

            return !empty;
        }

        public bool ModifyCountry(int id, string toModify, string value)
        {
            if((id is int) && (toModify is not null) && (value is not null) 
                && toModify.Length < 10 && value.Length < 30 && id > -1) 
            {
                Country country = _countryRepository.GetCountryById(id);
                bool status = false;
                if (country != null)
                {
                    switch (toModify)
                    {
                        case "area":
                            country.Area = double.Parse(value);
                            break;

                        case "population":
                            country.Population = int.Parse(value);
                            break;
                        case "name":
                            country.Name = value;
                            break;

                        case "capital":
                            country.Capital = value;
                            break;

                        default:
                            return false;
                            break;

                    }
                    status = _countryRepository.ModifyCountry(country);

                }
                return status;

            }
            else
            {
                return false;
            }
            
           
        }

    }
}
