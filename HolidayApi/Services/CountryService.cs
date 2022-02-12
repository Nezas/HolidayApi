using Newtonsoft.Json;

namespace HolidayApi.Services
{
    public class CountryService
    {
        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await RestService.Get<IEnumerable<Country>>("getSupportedCountries");
        }
    }
}
