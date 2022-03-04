namespace HolidayApi.BLL.Services
{
    /// <summary>
    /// All the logic of the holiday controller.
    /// </summary>
    public class HolidayService : IHolidayService
    {
        private readonly DataContext _db;

        public HolidayService(DataContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets all holidays of the given country and year from database. If database is empty, calls endpoint to fetch data and inserts it to the database.
        /// </summary>
        /// <returns> <see cref="IEnumerable{HolidayDto}"/> or null if the data was not found.</returns>
        public async Task<IEnumerable<HolidayDto>> GetHolidaysForYear(string country, int year)
        {
            if (_db.IsConnected == false)
            {
                return await RestService.Get<IEnumerable<HolidayDto>>($"getHolidaysForYear&year={year}&country={country}&holidayType=public_holiday");
            }

            var holidaysForYearDto = GetHolidaysFromDb(country, year);
            if (holidaysForYearDto != null)
            {
                return holidaysForYearDto;
            }

            var result = await RestService.Get<IEnumerable<HolidayDto>>($"getHolidaysForYear&year={year}&country={country}&holidayType=public_holiday");
            if (result == null)
            {
                return null;
            }

            AddHolidaysToDb(result, country);
            return result;
        }

        /// <summary>
        /// Adds holidays to the database.
        /// </summary>
        public void AddHolidaysToDb(IEnumerable<HolidayDto> holidaysDto, string countryCode)
        {
            foreach (var holidayDto in holidaysDto)
            {
                Holiday holiday = new();
                holiday.CountryCode = countryCode;
                holiday.Day = holidayDto.Date.Day;
                holiday.Month = holidayDto.Date.Month;
                holiday.Year = holidayDto.Date.Year;
                holiday.DayOfWeek = holidayDto.Date.DayOfWeek;
                holiday.HolidayType = holidayDto.HolidayType;

                _db.Holidays.Add(holiday);
                _db.SaveChanges();

                foreach (var holidayNamesDto in holidayDto.Name)
                {
                    HolidayName holidayName = new();
                    holidayName.Language = holidayNamesDto.Language;
                    holidayName.Text = holidayNamesDto.Text;
                    holidayName.CountryCode = countryCode;
                    holidayName.HolidayId = holiday.HolidayId;
                    _db.HolidayNames.Add(holidayName);
                }
            }
            _db.SaveChanges();
        }

        /// <summary>
        /// Gets holidays from the database.
        /// </summary>
        /// <returns> <see cref="List{HolidayDto}"/> or null if the data was not found.</returns>
        public List<HolidayDto> GetHolidaysFromDb(string country, int year)
        {
            var holidaysFromDb = _db.Holidays.Where(h => h.CountryCode == country && h.Year == year).ToList();
            if (holidaysFromDb.Count == 0)
            {
                return null;
            }

            List<HolidayDto> holidaysDto = new();

            foreach (var holiday in holidaysFromDb)
            {
                var holidayDto = new HolidayDto();
                var holidayDateDto = new HolidayDateDto();
                holidayDateDto.Day = holiday.Day;
                holidayDateDto.Month = holiday.Month;
                holidayDateDto.Year = holiday.Year;
                holidayDateDto.DayOfWeek = holiday.DayOfWeek;
                holidayDto.Date = holidayDateDto;

                var holidayNamesInDb = _db.HolidayNames.Where(hn => hn.HolidayId == holiday.HolidayId).ToList();
                holidayDto.Name = new HolidayNameDto[holidayNamesInDb.Count()];
                for (int i = 0; i < holidayNamesInDb.Count(); i++)
                {
                    HolidayNameDto holidayNameDto = new();
                    holidayNameDto.Language = holidayNamesInDb[i].Language;
                    holidayNameDto.Text = holidayNamesInDb[i].Text;
                    holidayDto.Name[i] = holidayNameDto;
                }

                holidayDto.HolidayType = holiday.HolidayType;

                holidaysDto.Add(holidayDto);
            }
            return holidaysDto;
        }
    }
}