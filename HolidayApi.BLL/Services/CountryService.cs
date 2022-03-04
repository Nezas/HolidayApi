namespace HolidayApi.BLL.Services
{
    /// <summary>
    /// All the logic of the country controller.
    /// </summary>
    public class CountryService : ICountryService
    {
        private readonly DataContext _db;

        public CountryService(DataContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Gets countries from the database. If database is empty, calls endpoint to fetch data and inserts it to the database.
        /// </summary>
        /// <returns> <see cref="IEnumerable{CountryDto}"/> or null if the data was not found </returns>
        public async Task<IEnumerable<CountryDto>> GetCountries()
        {
            if (_db.IsConnected == false)
            {
                return await RestService.Get<IEnumerable<CountryDto>>("getSupportedCountries");
            }

            var countriesDto = GetCountriesFromDb();
            if (countriesDto.Count() != 0)
            {
                return countriesDto;
            }

            var result = await RestService.Get<IEnumerable<CountryDto>>("getSupportedCountries");
            if (result == null)
            {
                return null;
            }

            AddCountriesToDb(result);
            return result;
        }

        /// <summary>
        /// Adds <see cref="IEnumerable{CountryDto}"/> to the database.
        /// </summary>
        /// <param name="countriesDto"> Countries data which was fetched from the endpoint.</param>
        public void AddCountriesToDb(IEnumerable<CountryDto> countriesDto)
        {
            foreach (var countryDto in countriesDto)
            {
                Country country = new();
                country.CountryCode = countryDto.CountryCode;

                foreach (var regionDto in countryDto.Regions)
                {
                    Region region = new Region();
                    region.Name = regionDto;
                    region.CountryCode = country.CountryCode;
                    _db.Regions.Add(region);
                }

                foreach (var holidayTypeDto in countryDto.HolidayTypes)
                {
                    HolidayType holidayType = new HolidayType();
                    holidayType.Name = holidayTypeDto;
                    holidayType.CountryCode = country.CountryCode;
                    _db.HolidayTypes.Add(holidayType);
                }

                country.FullName = countryDto.FullName;

                FromDate fromDate = new FromDate();
                fromDate.Day = countryDto.FromDate.Day;
                fromDate.Month = countryDto.FromDate.Month;
                fromDate.Year = countryDto.FromDate.Year;
                fromDate.CountryCode = country.CountryCode;
                _db.FromDates.Add(fromDate);

                ToDate toDate = new ToDate();
                toDate.Day = countryDto.ToDate.Day;
                toDate.Month = countryDto.ToDate.Month;
                toDate.Year = countryDto.ToDate.Year;
                toDate.CountryCode = country.CountryCode;
                _db.ToDates.Add(toDate);

                _db.Countries.Add(country);
            }
            _db.SaveChanges();
        }

        /// <summary>
        /// Gets countries data from the database.
        /// </summary>
        /// <returns> <see cref="IEnumerable{CountryDto}"/></returns>
        public IEnumerable<CountryDto> GetCountriesFromDb()
        {
            var countriesInDb = _db.Countries.ToList();

            foreach (var country in countriesInDb)
            {
                var countryDto = new CountryDto();
                countryDto.CountryCode = country.CountryCode;

                var regionsInDb = _db.Regions.Where(r => r.CountryCode == country.CountryCode).ToList();
                countryDto.Regions = new string[regionsInDb.Count()];
                for (int i = 0; i < regionsInDb.Count(); i++)
                {
                    countryDto.Regions[i] = regionsInDb[i].Name;
                }

                var holidayTypesInDb = _db.HolidayTypes.Where(ht => ht.CountryCode == country.CountryCode).ToList();
                countryDto.HolidayTypes = new string[holidayTypesInDb.Count()];
                for (int i = 0; i < holidayTypesInDb.Count(); i++)
                {
                    countryDto.HolidayTypes[i] = holidayTypesInDb[i].Name;
                }

                countryDto.FullName = country.FullName;

                var fromDateInDb = _db.FromDates.Single(d => d.CountryCode == country.CountryCode);
                countryDto.FromDate = new DateDto(fromDateInDb.Day, fromDateInDb.Month, fromDateInDb.Year);
                var toDateInDb = _db.ToDates.Single(d => d.CountryCode == country.CountryCode);
                countryDto.ToDate = new DateDto(toDateInDb.Day, toDateInDb.Month, toDateInDb.Year);

                yield return countryDto;
            }
        }
    }
}
