namespace HolidayApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("country")]
    public class CountryController : Controller
    {
        private readonly CountryService _countryService;

        public CountryController(CountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("/getCountries")]
        public async Task<IActionResult> GetCountries()
        {
            var result = await _countryService.GetCountries();
            return result == null ? NotFound() : Ok(result);
        }
    }
}
