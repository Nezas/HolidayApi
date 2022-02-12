namespace HolidayApi.Services
{
    public class HolidayService
    {
        public async Task<IEnumerable<Holiday>> GetHolidaysForYear(string country, int year)
        {
            return await RestService.Get<IEnumerable<Holiday>>($"getHolidaysForYear&year={year}&country={country}&holidayType=public_holiday");
        }
    }
}