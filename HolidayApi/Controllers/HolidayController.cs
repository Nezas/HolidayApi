namespace HolidayApi.Controllers
{
    /// <summary>
    /// Controller for holiday endpoints.
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("holiday")]
    public class HolidayController : Controller
    {
        private readonly IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        /// <summary>
        /// Gets all holidays for the given country and year.
        /// </summary>
        /// <response code="200">Returns all holidays</response>
        /// <response code="400">If the required parameters were missing</response>
        /// <response code="404">If the country or year were not found</response>  
        [HttpGet("getHolidaysForYear/")]
        public async Task<IActionResult> GetHolidaysForYear([FromQuery][Required] string country, [FromQuery][Required] int year)
        {
            var result = await _holidayService.GetHolidaysForYear(country, year);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
