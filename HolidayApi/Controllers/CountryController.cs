namespace HolidayApi.Controllers
{
    /// <summary>
    /// Controller for country endpoints.
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("country")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        /// <summary>
        /// Gets all supported countries.
        /// </summary>
        /// <response code="200">Returns all supported countries</response>
        /// <response code="404">If the countries were not found</response>  
        [HttpGet("getCountries/")]
        public async Task<IActionResult> GetCountries()
        {
            var result = await _countryService.GetCountries();
            return result == null ? NotFound() : Ok(result);
        }
    }
}
