using Microsoft.AspNetCore.Mvc;

namespace HolidayApi.Controllers
{
    [ApiController]
    [Route("holiday")]
    public class HolidayController : Controller
    {
        private readonly HolidayService _holidayService;

        public HolidayController(HolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        [HttpGet("getHolidaysForYear/{country}/{year}")]
        public async Task<IActionResult> GetHolidaysForYear(string country, int year)
        {
            return Ok(await _holidayService.GetHolidaysForYear(country, year));
        }
    }
}
