﻿namespace DayApi.Controllers
{
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

        [HttpGet("/getDayStatus")]
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

        [HttpGet("/getMaximumFreeDays")]
        public async Task<IActionResult> GetMaximumFreeDays([FromQuery][Required] string country, [FromQuery][Required] int year)
        {
            var result = await _dayService.GetMaximumFreeDays(country, year);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
