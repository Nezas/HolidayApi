namespace HolidayApi.Services
{
    public class HolidayService
    {
        public async Task<IEnumerable<HolidayDto>> GetHolidaysForYear(string country, int year)
        {
            return await RestService.Get<IEnumerable<HolidayDto>>($"getHolidaysForYear&year={year}&country={country}&holidayType=public_holiday");
        }
    }
}