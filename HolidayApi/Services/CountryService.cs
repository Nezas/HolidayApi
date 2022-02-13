namespace HolidayApi.Services
{
    public class CountryService : ICountryService
    {
        private readonly DataContext _db;

        public CountryService(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CountryDto>> GetCountries()
        {
            if (_db.Countries.Count() != 0)
            {
                return GetCountriesFromDb();
            }
            var result = await RestService.Get<IEnumerable<CountryDto>>("getSupportedCountries");
            AddCountriesToDb(result);
            return result;
        }

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
