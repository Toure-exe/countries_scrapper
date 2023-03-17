using Microsoft.AspNetCore.Mvc;
using PageScrapper.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PageScrapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IScrapperService _scrapperService;

        public CountryController(IScrapperService scrapperService)
        {
            _scrapperService = scrapperService;
        }


        // GET: api/<CountryController>
        [HttpGet]
        public IActionResult GetAllCountriesFromTarget()
        {
            IEnumerable<Country> result = _scrapperService.GetCountryListFromTarget("https://www.scrapethissite.com/pages/simple/");
            _scrapperService.SaveCountries();
            return result.Any() ? Ok(result) : NotFound("Couldn't get the countries list.");
            
        }

        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            Country result = _scrapperService.GetCountryById(id);
            if(result !=  null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<CountryController>
        [HttpPost]
        public IActionResult Post([FromBody] Country country)
        {
           return  _scrapperService.SaveCountry(country) ? Ok(country) : StatusCode(500); // Internal server error
        }

        // PUT api/<CountryController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] UpdatingOptions values, int id)
        {
            return _scrapperService.ModifyCountry(id, values.ToModify, values.Value) ? Ok() : StatusCode(500);
        }

        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCountryById(int id) =>
            _scrapperService.DeleteCountry(id) ? Ok() : NotFound("Country id is invalid");
      

        [HttpDelete]
        public IActionResult DeleteAllCountry() =>
            _scrapperService.DeleteAllCountriesFromDb() ? Ok() : NoContent();
     
    }
}
