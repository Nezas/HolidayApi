using Newtonsoft.Json;

namespace HolidayApi.Services
{
    public class CountryService
    {
        public async Task<IEnumerable<CountryDto>> GetCountries()
        {
            return await RestService.Get<IEnumerable<CountryDto>>("getSupportedCountries");
        }
    }
}
