using Microsoft.AspNetCore.Mvc;

namespace HolidayApi.Controllers
{
    [ApiController]
    [Route("country")]
    public class CountryController : Controller
    {
        private readonly CountryService _countryService;

        public CountryController(CountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("getCountries")]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(await _countryService.GetCountries());
        }
    }
}
