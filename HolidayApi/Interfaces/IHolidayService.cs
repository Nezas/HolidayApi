namespace HolidayApi.Interfaces
{
    public interface IHolidayService
    {
        void AddHolidaysToDb(IEnumerable<HolidayDto> holidaysDto, string countryCode);
        Task<IEnumerable<HolidayDto>> GetHolidaysForYear(string country, int year);
        List<HolidayDto> GetHolidaysFromDb(string country, int year);
    }
}