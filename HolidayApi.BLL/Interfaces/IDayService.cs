namespace HolidayApi.BLL.Interfaces
{
    public interface IDayService
    {
        void AddDayStatusToDb(string country, int year, int month, int day, string dayStatusResult);
        void AddMaximumFreeDaysToDb(string country, int year, int dayStatusResult);
        Task<DayStatusDto> GetDayStatus(string country, int year, int month, int day);
        DayStatusDto GetDayStatusFromDb(string country, int year, int month, int day);
        Task<IEnumerable<HolidayDto>> GetHolidaysForYear(string country, int year);
        Task<MaximumFreeDaysDto> GetMaximumFreeDays(string country, int year);
        MaximumFreeDaysDto GetMaximumFreeDaysFromDb(string country, int year);
        Task<JToken> IsPublicHoliday(string country, int year, int month, int day);
        Task<JToken> IsWorkDay(string country, int year, int month, int day);
    }
}