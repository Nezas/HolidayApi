namespace HolidayApi.BLL.Interfaces
{
    public interface ICountryService
    {
        void AddCountriesToDb(IEnumerable<CountryDto> countriesDto);
        Task<IEnumerable<CountryDto>> GetCountries();
        IEnumerable<CountryDto> GetCountriesFromDb();
    }
}