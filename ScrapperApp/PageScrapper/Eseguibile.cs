// See https://aka.ms/new-console-template for more information


using PageScrapper;
using Microsoft.Extensions.DependencyInjection;
using PageScrapper.Domain;
using PageScrapper.Infrastructure;
using Microsoft.EntityFrameworkCore;

string connString = "Data Source = XXXX\\MSSQLSERVER01; Initial Catalog= db_paesi; Integrated Security=true; TrustServerCertificate=True";


/*
 * add-migration InitialMigration (crea le migrazioni)
 * update-database (crea la tabella)
 */
var serviceProvider = new ServiceCollection()
                        .AddScoped<IDownloaderService,DownloaderService>()
                        .AddScoped<IScrapperService, ScrapperService>()
                        .AddScoped<ICountryRepository, CountryRepository>()
                        .AddDbContext<CountryDbContext>(opt => opt.UseSqlServer(connString))
                        .BuildServiceProvider();

var scrapper = serviceProvider.GetService<IScrapperService>();

IEnumerable<Country> list = scrapper.GetCountryListFromTarget("https://www.scrapethissite.com/pages/simple/");
//scrapper.SaveCountries();

foreach (Country country in list)
{
    Console.WriteLine($" NOME:  {country.Name} \n CAPITALE: {country.Capital} \n POPOLAZIONE: {country.Population}\n AREA: {country.Area} km^2 \n______\n");
}
scrapper.DeleteAllCountriesFromDb();

