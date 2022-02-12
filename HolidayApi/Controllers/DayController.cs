using System.ComponentModel.DataAnnotations;

namespace DayApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("day")]
    public class DayController : Controller
    {
        private readonly DayService _dayService;

        public DayController(DayService dayService)
        {
            _dayService = dayService;
        }

        [HttpGet("/getDayStatus")]
        public async Task<IActionResult> GetDayStatus([FromQuery][Required] string country, [FromQuery][Required] int year, [FromQuery][Required] int month, [FromQuery][Required] int day)
        {
            var result = await _dayService.GetDayStatus(country, year, month, day);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("/getMaximumFreeDays")]
        public async Task<IActionResult> GetMaximumFreeDays([FromQuery][Required] string country, [FromQuery][Required] int year)
        {
            var result = await _dayService.GetMaximumFreeDays(country, year);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
