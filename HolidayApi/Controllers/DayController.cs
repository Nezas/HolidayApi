namespace DayApi.Controllers
{
    /// <summary>
    /// Controller for day endpoints.
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("day")]
    public class DayController : Controller
    {
        private readonly IDayService _dayService;

        public DayController(IDayService dayService)
        {
            _dayService = dayService;
        }

        /// <summary>
        /// Gets day status for the given country and date.
        /// </summary>
        /// <response code="200">Returns the day status</response>
        /// <response code="400">If the required parameters were missing</response>
        /// <response code="404">If the day was not found</response>  
        [HttpGet("getDayStatus/")]
        public async Task<IActionResult> GetDayStatus([FromQuery][Required] string country, [FromQuery][Required] int year, [FromQuery][Required] int month, [FromQuery][Required] int day)
        {
            try
            {
                var result = await _dayService.GetDayStatus(country, year, month, day);
                return result == null ? NotFound() : Ok(result);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Gets maximum free days in a row for the given country and year.
        /// </summary>
        /// <response code="200">Returns the maximum free days</response>
        /// <response code="400">If the required parameters were missing</response>
        /// <response code="404">If the country or year were not found</response> 
        [HttpGet("getMaximumFreeDays/")]
        public async Task<IActionResult> GetMaximumFreeDays([FromQuery][Required] string country, [FromQuery][Required] int year)
        {
            var result = await _dayService.GetMaximumFreeDays(country, year);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
