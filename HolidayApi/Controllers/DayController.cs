using HolidayApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DayApi.Controllers
{
    [ApiController]
    [Route("day")]
    public class DayController : Controller
    {
        private readonly DayService _dayService;

        public DayController(DayService dayService)
        {
            _dayService = dayService;
        }

        [HttpGet("getDayStatus/{country}/{year}/{month}/{day}")]
        public async Task<IActionResult> GetDayStatus(string country, int year, int month, int day)
        {
            return Ok(_dayService.GetDayStatus(country, year, month, day));
        }

        [HttpGet("getMaximumFreeDays/{country}/{year}/")]
        public async Task<IActionResult> GetMaximumFreeDays(string country, int year)
        {
            return Json(_dayService.GetMaximumFreeDays(country, year));
        }
    }
}
