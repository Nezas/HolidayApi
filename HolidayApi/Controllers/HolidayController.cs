﻿namespace HolidayApi.Controllers
{
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

        [HttpGet("getHolidaysForYear/")]
        public async Task<IActionResult> GetHolidaysForYear([FromQuery][Required] string country, [FromQuery][Required] int year)
        {
            var result = await _holidayService.GetHolidaysForYear(country, year);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
